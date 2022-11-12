using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GAction
{
    // Target where the action is going to take place
    public GameObject target;
    public Vector3 destination;
    // Dictionary of preconditions
    public Dictionary<string, int> preconditions;
    // Dictionary of effects
    public Dictionary<string, int> effects;
    // State of the agent
    public WorldStates agentBeliefs;

    // public Inventory inventory;
    public WorldStates beliefs;

    // Are we currently performing an action?
    public bool running = false;


    public GActionSObject action;

    // Name of the action
    public string actionName { get { return action?.actionName ?? ""; } }
    // Cost of the action
    public float cost { get { return action.cost; } }
    // For game objects with tags
    public string targetTag { get { return action.targetTag; } }
    // Duration the action should take
    public float duration { get { return action.duration; } }


    public GAction(GActionSObject action, /*Inventory inventory, */ WorldStates beliefs)

    {
        this.action = action;

        // Set up the preconditions and effects
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();

        // Check validity of preConditions
        if (action.preConditions != null)
        {

            foreach (WorldState w in action.preConditions)
            {
                // Add each item to our Dictionary
                preconditions.Add(w.key, w.value);
            }
        }

        // Check validity of afterEffects
        if (action.afterEffects != null)
        {

            foreach (WorldState w in action.afterEffects)
            {
                // Add each item to our Dictionary
                effects.Add(w.key, w.value);
            }
        }

        // this.inventory = inventory;
        this.beliefs = beliefs;
    }

    public bool PrePerform(GAgent agent)
    {
        return action.PrePerform(agent, this);
    }

    public bool PostPerform(GAgent agent)
    {
        return action.PostPerform(agent, this);
    }

    public bool IsAchievable()
    {

        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {

        foreach (KeyValuePair<string, int> p in preconditions)
        {

            if (!conditions.ContainsKey(p.Key))
            {

                return false;
            }
        }
        return true;
    }

    public bool WillSatisfyGiven(Dictionary<string, int> conditions)
    {

        foreach (KeyValuePair<string, int> p in effects)
        {

            if (conditions.ContainsKey(p.Key))
            {
                return true;
            }
        }
        return false;
    }

}