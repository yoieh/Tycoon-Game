using UnityEngine;

namespace Worker.Buffs
{
    // decreases damage
    [CreateAssetMenu(fileName = "Armor", menuName = "Worker/Buffs/Armor")]
    public class ArmorBuff : BaseBuff
    {
        public float armor;
        public new float ModifyDamage(float damage)
        {
            damage -= armor; //decreases damage by armor
            return damage > 0 ? damage : 0; //make sure damage is always positive
        }
    }
}