using System.Collections.Generic;
using UnityEngine;

namespace GOAP
{
    using Actions;
    public abstract class GActionSObject : ScriptableObject
    {

        // Name of the action
        public string actionName = "Action";
        
        // Target where the action is going to take place
        public string targetTag;
        // Duration the action should take
        public int duration = 1;

        public WorldState[] costConditions;

        // An array of WorldStates of preconditions
        public WorldState[] preConditions;
        // An array of WorldStates of afterEffects
        public WorldState[] afterEffects;

        public abstract bool PrePerform(GAgent agent, GAction action);
        public abstract bool PostPerform(GAgent agent, GAction action);
    }
}