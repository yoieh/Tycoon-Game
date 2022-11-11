using System;
using System.Collections.Generic;
using UnityEngine;

namespace Worker
{
    using Buffs;

    public class WorkerStats : MonoBehaviour
    {
        // properties of character
        [SerializeField] private float health;
        [SerializeField] private string stateName;

        public event Action<float> workerDamaged; // event to notify damage

        public List<BaseBuff> workerBuffs;

        // Deal damage, damage will be reduced by armour
        public void Damage(float damage)
        {
            // modify damage according to each buff
            foreach (BaseBuff buff in workerBuffs)
            {
                damage = buff.ModifyDamage(damage);
            }

            // damage health
            health -= damage;
            workerDamaged?.Invoke(health);
        }

        // get current health
        public float GetHealth()
        {
            return health;
        }

        // get Wwrker's name
        public string GetName()
        {
            return name;
        }
    }
}
