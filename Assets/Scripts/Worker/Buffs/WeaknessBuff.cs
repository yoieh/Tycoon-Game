using UnityEngine;

namespace Worker.Buffs
{
    // it increases damage by
    [CreateAssetMenu(fileName = "Weakness", menuName = "Worker/Buffs/Weakness")]
    public class WeaknessBuff : BaseBuff
    {
        public float weaknessAmount;
        public new float ModifyDamage(float damage)
        {
            damage *= weaknessAmount; // increasing damage
            return damage;
        }
    }
}