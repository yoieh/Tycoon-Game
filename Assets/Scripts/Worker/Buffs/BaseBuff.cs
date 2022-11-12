using System;
using System.Collections.Generic;
using UnityEngine;

namespace Worker.Buffs
{
    // abstract class to implement buff
    public class BaseBuff : ScriptableObject
    {
        // modifies damage dealt
        public float ModifyDamage(float damage)
        {
            return damage;
        }

        public float ModifyHealing(float health)
        {
            return health;
        }

        public float ModifyHappiness(float happiness)
        {
            return happiness;
        }

        public float ModifyEnergy(float energy)
        {
            return energy;
        }

        public float ModifyWater(float water)
        {
            return water;
        }

        public float ModifyFood(float food)
        {
            return food;
        }


    }

}
