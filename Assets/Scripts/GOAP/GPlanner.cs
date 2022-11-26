using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace GOAP
{
    using Actions;
    public class Node
    {
        public Node parent;
        public Dictionary<WorldStateTypes, int> state;
        public Dictionary<WorldStateTypes, int> constState;
        public GAction action;

        public Node(Node parent, Dictionary<WorldStateTypes, int> allstates, GAction action)
        {
            this.parent = parent;
            this.constState = new Dictionary<WorldStateTypes, int>();
            this.state = new Dictionary<WorldStateTypes, int>(allstates);
            this.action = action;
        }

        public Node(Node parent, Dictionary<WorldStateTypes, int> constState, Dictionary<WorldStateTypes, int> allstates, GAction action)
        {
            this.parent = parent;
            this.constState = new Dictionary<WorldStateTypes, int>(constState);
            this.state = new Dictionary<WorldStateTypes, int>(allstates);
            this.action = action;
        }

        public Node(Node parent, Dictionary<WorldStateTypes, int> constState, Dictionary<WorldStateTypes, int> allstates, Dictionary<WorldStateTypes, int> beliefstates, GAction action)
        {
            this.parent = parent;
            this.constState = constState;
            this.state = new Dictionary<WorldStateTypes, int>(allstates);

            foreach (KeyValuePair<WorldStateTypes, int> b in beliefstates)
            {
                if (!this.state.ContainsKey(b.Key))
                {
                    this.state.Add(b.Key, b.Value);
                }
            }

            this.action = action;
        }

        public int Cost()
        {
            int cost = 0;
            foreach (KeyValuePair<WorldStateTypes, int> s in constState)
            {
                if (state.ContainsKey(s.Key))
                {
                    cost += Mathf.Abs(state[s.Key] - s.Value);
                }
            }

            return cost;
        }
    }

    public class GPlanner
    {
        public int runs = 0;

        public Queue<GAction> plan(List<GAction> actions, Dictionary<WorldStateTypes, int> goal, WorldStates belifestates)
        {
            // List<GAction> usableActions = new List<GAction>(actions);
            // foreach (GAction a in actions)
            // {
            //     if (a.IsAchievable())
            //         usableActions.Add(a);
            // }

            List<Node> leaves = new List<Node>();

            Node end = new Node(null, goal, null);
            bool success = BuildGraph(end, leaves, actions, goal, belifestates);

            if (!success)
            {
                // string goals = "";
                // foreach (KeyValuePair<string, int> g in goal)
                // {
                //     goals += g.Key + " : " + g.Value + ", ";
                // }
                // Debug.Log("NO PLAN " + goals);
                return null;
            }

            Node cheapest = null;
            foreach (Node leaf in leaves)
            {
                if (cheapest == null)
                    cheapest = leaf;
                else
                {
                    if (leaf.Cost() < cheapest.Cost())
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

        private bool BuildGraph(Node parent, List<Node> leaves, List<GAction> usuableActions, Dictionary<WorldStateTypes, int> goal, WorldStates beliefs)
        {
            bool foundPath = false;

            foreach (GAction action in usuableActions)
            {
                if (action.WillSatisfyGiven(parent.state))
                {
                    Dictionary<WorldStateTypes, int> currentState = new Dictionary<WorldStateTypes, int>(parent.state);
                    foreach (KeyValuePair<WorldStateTypes, int> eff in action.preConditions)
                    {
                        if (!currentState.ContainsKey(eff.Key))
                            currentState.Add(eff.Key, eff.Value);
                    }

                    Dictionary<WorldStateTypes, int> constState = new Dictionary<WorldStateTypes, int>(parent.constState);
                    foreach (KeyValuePair<WorldStateTypes, int> eff in action.effects)
                    {
                        if (!constState.ContainsKey(eff.Key))
                            constState.Add(eff.Key, eff.Value);
                    }


                    Node node = new Node(parent, constState, currentState, action);

                    bool isAchievable = node.action.IsAchievableGiven(beliefs.GetStates());
                    if (isAchievable)
                    {
                        leaves.Add(node);
                        foundPath = true;
                    }
                    else
                    {
                        List<GAction> subset = ActionSubset(usuableActions, action);
                        if (BuildGraph(node, leaves, subset, goal, beliefs))
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
}
