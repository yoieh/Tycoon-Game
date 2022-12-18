using System;
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
public class Inventory
{
    [SerializeField] private List<ItemType> _alowedItemTypes;
    [SerializeField] private int _maxAmount;
    [SerializeField]
    private Dictionary<ItemType, ItemStack> _itemsSlots = new Dictionary<ItemType, ItemStack>();

    public int StoredAmount { get { return TotalAmount(); } }

    public int MaxAmount { get { return _maxAmount; } }

    public Dictionary<ItemType, ItemStack> ItemsSlots { get { return _itemsSlots; } }

    public Inventory() { }

    public Inventory(List<ItemType> alowedItemTypes, int maxAmount)
    {
        _alowedItemTypes = alowedItemTypes;
        _maxAmount = maxAmount;
    }

    public Inventory(List<ItemType> alowedItemTypes)
    {
        _alowedItemTypes = alowedItemTypes;
    }

    public Inventory(int maxAmount)
    {
        _maxAmount = maxAmount;
    }

    public bool SetItem(ItemType itemType, int amount)
    {
        if (!CheckItemType(itemType)) return false;

        if (!CheckItemAmount(amount)) return false;

        if (!_itemsSlots.ContainsKey(itemType))
        {
            _itemsSlots.Add(itemType, new ItemStack { ItemType = itemType, Amount = amount });
            return true;
        }

        _itemsSlots[itemType] = _itemsSlots[itemType] + amount;
        return true;
    }

    public bool SetItem(ItemStack itemStack)
    {
        if (!CheckItemType(itemStack.ItemType)) return false;

        if (!CheckItemAmount(itemStack.Amount)) return false;

        if (!_itemsSlots.ContainsKey(itemStack.ItemType))
        {
            _itemsSlots.Add(itemStack.ItemType, itemStack);
            return true;
        }

        _itemsSlots[itemStack.ItemType] = _itemsSlots[itemStack.ItemType] + itemStack.Amount;
        return true;
    }

    // Gets items from item stack in inventory
    public ItemStack? GetItem(ItemType itemType, int amount)
    {
        // Check Item Type - Returns false if not valid
        if (!HasItem(itemType)) return null;

        if (_itemsSlots[itemType] < amount) amount = _itemsSlots[itemType].Amount;

        _itemsSlots[itemType] = _itemsSlots[itemType] - amount;
        return new ItemStack { ItemType = itemType, Amount = amount };
    }

    public ItemStack? GetItem(ItemType itemType)
    {
        if (!HasItem(itemType)) return null;

        return GetItem(itemType, _itemsSlots[itemType].Amount);
    }

    public ItemStack? GetItem(ItemStack itemStack)
    {
        return GetItem(itemStack.ItemType, itemStack.Amount);
    }

    public bool RemoveItem(ItemStack itemStack)
    {
        if (!HasItem(itemStack)) return false;

        if (_itemsSlots[itemStack.ItemType] == itemStack)
        {
            _itemsSlots.Remove(itemStack.ItemType);
            return true;
        }

        _itemsSlots[itemStack.ItemType] = _itemsSlots[itemStack.ItemType] - itemStack.Amount;
        return true;
    }


    public bool HasItem(ItemType itemType, int amount)
    {
        if (!CheckItemType(itemType)) return false;

        if (!_itemsSlots.ContainsKey(itemType)) return false;

        return _itemsSlots[itemType] >= amount;
    }

    public bool HasItem(ItemType itemType)
    {
        return HasItem(itemType, 0);
    }

    public bool HasItem(ItemStack itemStack)
    {
        if (!HasItem(itemStack.ItemType, itemStack.Amount)) return false;

        return _itemsSlots[itemStack.ItemType] >= itemStack;
    }

    private bool CheckItemType(ItemType itemType)
    {
        // If none itemtypes are in the list, then all itemtypes are allowed
        if (_alowedItemTypes.Count == 0) return true;

        return _alowedItemTypes.Contains(itemType);
    }

    private bool CheckItemAmount(int amount)
    {
        // If max amount is -1, then there is no limit
        if (_maxAmount == -1) return true;

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
}
