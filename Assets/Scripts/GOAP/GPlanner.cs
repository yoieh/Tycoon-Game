﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    public Node(Node parent, float cost, Dictionary<string, int> allstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);
        this.action = action;
    }

    public Node(Node parent, float cost, Dictionary<string, int> allstates, Dictionary<string, int> beliefstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);

        foreach (KeyValuePair<string, int> b in beliefstates)
        {
            if (!this.state.ContainsKey(b.Key))
            {
                this.state.Add(b.Key, b.Value);
            }
        }

        this.action = action;
    }
}

public class GPlanner
{
    public int runs = 0;

    public Queue<GAction> plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates belifestates)
    {
        // List<GAction> usableActions = new List<GAction>(actions);
        // foreach (GAction a in actions)
        // {
        //     if (a.IsAchievable())
        //         usableActions.Add(a);
        // }

        runs = 0;

        List<Node> leaves = new List<Node>();

        Node end = new Node(null, 0, goal, null);
        bool success = BuildGraph(end, leaves, actions, goal);

        Debug.Log("BuildGraph: " + runs);

        if (!success)
        {
            Debug.Log("NO PLAN");
            return null;
        }

        Node cheapest = null;
        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else
            {
                if (leaf.cost < cheapest.cost)
                    cheapest = leaf;
            }
        }

        List<GAction> result = new List<GAction>();
        Node n = cheapest;
        while (n != null)
        {
            if (n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<GAction> queue = new Queue<GAction>();
        foreach (GAction a in result.Reverse<GAction>())
        {
            queue.Enqueue(a);
        }

        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usuableActions, Dictionary<string, int> goal)
    {
        runs += 1;

        bool foundPath = false;

        foreach (GAction action in usuableActions)
        {
            if (action.WillSatisfyGiven(parent.state))
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach (KeyValuePair<string, int> eff in action.preconditions)
                {
                    if (!currentState.ContainsKey(eff.Key))
                        currentState.Add(eff.Key, eff.Value);
                }

                Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                bool isAchievable = node.action.IsAchievableGiven(GWorld.Instance.GetWorld().GetStates());
                if (isAchievable)
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GAction> subset = ActionSubset(usuableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                        foundPath = true;
                }
            }
        }

        return foundPath;
    }

    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe)
    {
        List<GAction> subset = new List<GAction>();
        foreach (GAction a in actions)
        {
            if (!a.Equals(removeMe))
                subset.Add(a);
        }
        return subset;
    }

}