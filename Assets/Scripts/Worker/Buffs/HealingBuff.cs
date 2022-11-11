using UnityEngine;

namespace Worker.Buffs
{
    // this buff heals amount equal to damage
    [CreateAssetMenu(fileName = "Healing", menuName = "Worker/Buffs/Healing")]
    public class HealingBuff : BaseBuff
    {
        public new float ModifyDamage(float damage)
        {
            if (damage > 0) damage = -damage; // make damage negative so that it will heal

            return -damage;
        }
    }
}