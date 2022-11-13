using UnityEngine;
using GOAP.Stats.Buffs;

namespace Worker.Buffs
{
    // this buff heals amount equal to damage
    [CreateAssetMenu(fileName = "Healing", menuName = "Worker/Buffs/Healing")]
    public class HealingBuff : GBaseBuff
    {
        public new float ModifyDamage(float damage)
        {
            if (damage > 0) damage = -damage; // make damage negative so that it will heal

            return -damage;
        }
    }
}