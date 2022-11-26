using UnityEngine;
using GOAP.Stats.Buffs;

namespace Worker.Buffs
{
    // it increases damage by
    [CreateAssetMenu(fileName = "Weakness", menuName = "Worker/Buffs/Weakness")]
    public class WeaknessBuff : GBaseBuff
    {
        public int weaknessAmount;
        public new int ModifyDamage(int damage)
        {
            damage *= weaknessAmount; // increasing damage
            return damage;
        }
    }
}