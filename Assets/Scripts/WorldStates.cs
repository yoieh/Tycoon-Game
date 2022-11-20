using System;
using System.Collections.Generic;


public enum WorldStateTypes
{
    None,


    DeliveredFood,
    HasFood,


    DeliveredGold,
    HasGold,


    DeliveredIron,
    HasIron,


    DeliveredStone,
    HasStone,


    DeliveredWater,
    HasWater,


    DeliveredWood,
    HasWood,


    /* Agent Stat World States */
    // Energy
    IsTired,
    HasRested,
    Energy,

    // Hunger
    IsHungry,
    HasEaten,
    Food,

    // Thirst
    IsThirsty,
    HasDrank,
    Water,

    // Happiness
    IsSad,
    HasGotenHappy,
    Happiness,

    // Health
    Health
}

[System.Serializable]
public struct WorldState
{
    public WorldStateTypes key;
    public int value;

    public static WorldStateTypes ByName(string name)
    {
        return (WorldStateTypes)System.Enum.Parse(typeof(WorldStateTypes), name);
    }
}

public class WorldStates
{

    // Constructor
    public Dictionary<WorldStateTypes, int> states;

    public WorldStates()
    {

        states = new Dictionary<WorldStateTypes, int>();
    }

    /************** Helper funtions ****************/
    // Check for a key
    public bool HasState(WorldStateTypes key)
    {

        return states.ContainsKey(key);
    }

    // Add to our dictionary
    private void AddState(WorldStateTypes key, int value)
    {

        states.Add(key, value);
    }

    public void ModifyState(WorldStateTypes key, int value)
    {

        // If it contains this key
        if (HasState(key))
        {

            // Add the value to the state
            states[key] += value;
            // If it's less than zero then remove it
            if (states[key] <= 0)
            {

                // Call the RemoveState method
                RemoveState(key);
            }
        }
        else
        {

            AddState(key, value);
        }
    }

    // Method to remove a state
    private void RemoveState(WorldStateTypes key)
    {

        // Check if it frist exists
        if (HasState(key))
        {

            states.Remove(key);
        }
    }

    // Set a state
    public void SetState(WorldStateTypes key, int value)
    {

        // Check if it exists
        if (HasState(key))
        {

            states[key] = value;
        }
        else
        {

            AddState(key, value);
        }
    }

    public Dictionary<WorldStateTypes, int> GetStates()
    {

        return states;
    }

    public WorldStates Merge(WorldStates beliefStates)
    {
        foreach (KeyValuePair<WorldStateTypes, int> belief in beliefStates.GetStates())
        {

            if (!HasState(belief.Key))
            {

                states.Add(belief.Key, belief.Value);
            }
        }

        return this;
    }
}
