using System.Collections.Generic;
using UnityEngine;

public abstract class GActionSObject : ScriptableObject
{

    // Name of the action
    public string actionName = "Action";
    // Cost of the action
    public float cost = 1.0f;
    // Target where the action is going to take place
    public string targetTag;
    // Duration the action should take
    public float duration = 0.0f;
    // An array of WorldStates of preconditions
    public WorldState[] preConditions;
    // An array of WorldStates of afterEffects
    public WorldState[] afterEffects;

    public abstract bool PrePerform(GAgent agent, GAction action);
    public abstract bool PostPerform(GAgent agent, GAction action);
}
