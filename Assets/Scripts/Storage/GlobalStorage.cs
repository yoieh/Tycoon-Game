using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalStorage : MonoBehaviourSingleton<GlobalStorage>
{
    // Dictionary for storing the global values
    [SerializeField] private Inventory _inventory = new Inventory();

    public static Inventory Inventory { get { return Instance._inventory; } }

    public static bool AddItemToInventory(ItemType itemType, int amount)
    {
        return Instance._inventory.SetItem(itemType, amount);
    }

    public static ItemStack? GetItemFromInventory(ItemType itemType, int amount)
    {
        return Instance._inventory.GetItem(itemType, -amount);
    }

    public static ItemStack? GetItemFromInventory(ItemType itemType)
    {
        return Instance._inventory.GetItem(itemType);
    }
}

