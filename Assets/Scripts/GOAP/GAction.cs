using System;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP.Actions
{
    [Serializable]
    public class GAction
    {
        // Target where the action is going to take place
        public GameObject target;
        public Vector3 destination;

        public Dictionary<WorldStateTypes, int> costConditions;

        // Dictionary of preConditions
        public Dictionary<WorldStateTypes, int> preConditions;
        // Dictionary of effects
        public Dictionary<WorldStateTypes, int> effects;

        // public Inventory inventory;

        // State of the agent
        public WorldStates beliefs;

        // Are we currently performing an action?
        public bool running = false;


        public GActionSObject action;

        // Name of the action
        public string actionName { get { return action?.actionName ?? ""; } }

        // For game objects with tags
        public string targetTag { get { return action.targetTag; } }
        // Duration the action should take
        public int duration { get { return action.duration; } }

        public GAction(GActionSObject action, /*Inventory inventory, */ WorldStates beliefs)

        {
            this.action = action;

            costConditions = new Dictionary<WorldStateTypes, int>();

            // Set up the preconditions and effects
            preConditions = new Dictionary<WorldStateTypes, int>();
            effects = new Dictionary<WorldStateTypes, int>();

            // Check validity of preConditions
            if (action.preConditions != null)
            {

                foreach (WorldState w in action.preConditions)
                {
                    // Add each item to our Dictionary
                    preConditions.Add(w.key, w.value);
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

            // Check validity of costConditions
            if (action.costConditions != null)
            {

                foreach (WorldState w in action.costConditions)
                {
                    // Add each item to our Dictionary
                    costConditions.Add(w.key, w.value);
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
            bool preformad = action.PostPerform(agent, this);

            if (!preformad)
                return false;

            // trigger preformadAction event
            agent.OnActionPreformad(this);
            return preformad;
        }

        public bool IsAchievable()
        {

            return true;
        }

        // Must defualt to true 
        public bool IsAchievableGiven(Dictionary<WorldStateTypes, int> conditions)
        {

            // must have all preconditions
            foreach (KeyValuePair<WorldStateTypes, int> p in preConditions)
            {
                if (!conditions.ContainsKey(p.Key))
                {
                    return false;
                }
            }

            bool _isAchievable = true;
            // must have all costConditions
            foreach (KeyValuePair<WorldStateTypes, int> p in costConditions)
            {
                if (!conditions.ContainsKey(p.Key))
                {
                    _isAchievable = false;
                    break;
                }
                else
                {
                    if (-1 * costConditions[p.Key] > conditions[p.Key])
                        return false;
                }
            }

            return _isAchievable;
        }

        // Must defualt to false
        public bool WillSatisfyGiven(Dictionary<WorldStateTypes, int> conditions)
        {
            bool _willSatisfy = false;

            foreach (KeyValuePair<WorldStateTypes, int> p in effects)
            {

                if (conditions.ContainsKey(p.Key))
                {
                    _willSatisfy = true;
                }
            }

            return _willSatisfy;
        }

    }
}