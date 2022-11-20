using System;
using System.Collections.Generic;
using UnityEngine;

namespace GOAP.Stats.Buffs
{
    // abstract class to implement buff
    public class GBaseBuff : ScriptableObject
    {
        // modifies damage dealt
        public int ModifyDamage(int damage)
        {
            return damage;
        }

        public int ModifyHealing(int health)
        {
            return health;
        }

        public int ModifyHappiness(int happiness)
        {
            return happiness;
        }

        public int ModifyEnergy(int energy)
        {
            return energy;
        }

        public int ModifyWater(int water)
        {
            return water;
        }

        public int ModifyFood(int food)
        {
            return food;
        }


    }

}
