using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food,
    Water,
    Wood,
    Stone,
    Iron,
    Gold
}



[System.Serializable]
public struct ItemStack
{
    public ItemType ItemType;
    public int Amount;

    public ItemStack(ItemType itemType, int amount)
    {
        ItemType = itemType;
        Amount = amount;
    }

    public static bool operator ==(ItemStack aStack, ItemStack bStack) => aStack.Amount == bStack.Amount;
    public static bool operator !=(ItemStack aStack, ItemStack bStack) => aStack.Amount != bStack.Amount;

    public static bool operator >(ItemStack aStack, ItemStack bStack) => aStack.Amount > bStack.Amount;
    public static bool operator <(ItemStack aStack, ItemStack bStack) => aStack.Amount < bStack.Amount;

    public static bool operator >=(ItemStack aStack, ItemStack bStack) => aStack.Amount >= bStack.Amount;
    public static bool operator <=(ItemStack aStack, ItemStack bStack) => aStack.Amount <= bStack.Amount;

    public static ItemStack operator +(ItemStack aStack, ItemStack bStack) => new ItemStack(aStack.ItemType, aStack.Amount + bStack.Amount);
    public static ItemStack operator -(ItemStack aStack, ItemStack bStack) => new ItemStack(aStack.ItemType, aStack.Amount - bStack.Amount);

    public static ItemStack operator *(ItemStack aStack, ItemStack bStack) => new ItemStack(aStack.ItemType, aStack.Amount * bStack.Amount);

    public static ItemStack operator ++(ItemStack aStack) => new ItemStack(aStack.ItemType, aStack.Amount + 1);
    public static ItemStack operator --(ItemStack aStack) => new ItemStack(aStack.ItemType, aStack.Amount - 1);

    public static bool operator ==(ItemStack aStack, int bAmount) => aStack.Amount == bAmount;
    public static bool operator !=(ItemStack aStack, int bAmount) => aStack.Amount != bAmount;

    public static bool operator >(ItemStack aStack, int bAmount) => aStack.Amount > bAmount;
    public static bool operator <(ItemStack aStack, int bAmount) => aStack.Amount < bAmount;

    public static bool operator >=(ItemStack aStack, int bAmount) => aStack.Amount >= bAmount;
    public static bool operator <=(ItemStack aStack, int bAmount) => aStack.Amount <= bAmount;

    public static ItemStack operator +(ItemStack aStack, int bAmount) => new ItemStack(aStack.ItemType, aStack.Amount + bAmount);
    public static ItemStack operator -(ItemStack aStack, int bAmount) => new ItemStack(aStack.ItemType, aStack.Amount - bAmount);

    public static ItemStack operator *(ItemStack aStack, int bAmount) => new ItemStack(aStack.ItemType, aStack.Amount * bAmount);

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return ItemType.ToString();
    }
}

[System.Serializable]
public class Inventory
{
    [SerializeField] private List<ItemType> _alowedItemTypes;
    [SerializeField] private int _maxAmount;
    [SerializeField]
    private Dictionary<ItemType, ItemStack> _itemsSlots = new Dictionary<ItemType, ItemStack>();

    public int StoredAmount { get { return TotalAmount(); } }

    public bool SetItem(ItemType itemType, int amount)
    {

        // Check Item Type - Returns false if not valid
        if (!CheckItemType(itemType)) return false;

        // Check Item Amount - Returns false if not valid
        if (!CheckItemAmount(amount)) return false;

        // 3. Adds to stored items
        if (!_itemsSlots.ContainsKey(itemType))
        {
            _itemsSlots.Add(itemType, new ItemStack { ItemType = itemType, Amount = amount });
            return true;
        }

        _itemsSlots[itemType] = _itemsSlots[itemType] + amount;
        return true;
    }

    public ItemStack GetItem(ItemType itemType, int amount)
    {
        // Check Item Type - Returns false if not valid
        if (!CheckItemType(itemType)) return new ItemStack(itemType, 0);

        if (!_itemsSlots.ContainsKey(itemType)) return new ItemStack(itemType, 0);

        if (_itemsSlots[itemType] < amount) amount = _itemsSlots[itemType].Amount;

        _itemsSlots[itemType] = _itemsSlots[itemType] - amount;
        return new ItemStack { ItemType = itemType, Amount = amount };
    }

    public ItemStack GetItem(ItemType itemType)
    {
        return GetItem(itemType, _itemsSlots[itemType].Amount);
    }

    // Should Retrave A World state of stored items that can be used GAgent Beliefs in GOAP Planner
    public WorldStates GetStoredItemState()
    {
        WorldStates states = new WorldStates();
        foreach (var item in _itemsSlots)
        {
            if (item.Value > 0)
            {
                WorldStateTypes stateType = WorldState.ByName("Has" + item.ToString());
                states.SetState(stateType, item.Value.Amount);
            }
        }

        return states;
    }

    private bool CheckItemType(ItemType itemType)
    {
        return _alowedItemTypes.Contains(itemType);
    }

    private bool CheckItemAmount(int amount)
    {
        return TotalAmount() + amount <= _maxAmount;
    }

    private int TotalAmount()
    {
        int total = 0;
        foreach (var item in _itemsSlots)
        {
            total += item.Value.Amount;
        }

        return total;
    }
}
