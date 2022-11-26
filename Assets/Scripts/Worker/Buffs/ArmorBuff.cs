using UnityEngine;
using GOAP.Stats.Buffs;

namespace Worker.Buffs
{
    // decreases damage
    [CreateAssetMenu(fileName = "Armor", menuName = "Worker/Buffs/Armor")]
    public class ArmorBuff : GBaseBuff
    {
        public int armor;
        public new int ModifyDamage(int damage)
        {
            damage -= armor; //decreases damage by armor
            return damage > 0 ? damage : 0; //make sure damage is always positive
        }
    }
}