using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Resource
{
    public class ResourceSourceAgent : MonoBehaviour
    {
        public event Action<int> amountUpdated;
        public Resource ResourceSource;
        [SerializeField] private SpriteRenderer spriteRenderer;
        // [SerializeField] private int amount;
        public int Amount { get { return inventory.StoredAmount; } }

        [SerializeField] private Inventory inventory = new Inventory();

        public ItemStack? Harvest()
        {
            if (Amount <= 0) return null;

            int harvestAmount = ResourceSource.Harvest();
            // Returns null if harvest failed
            if (harvestAmount <= 0) return null;

            bool hasItem = inventory.HasItem(ResourceSource.ResourceType, harvestAmount);

            if (!hasItem) return null;

            ItemStack? item = inventory.GetItem(ResourceSource.ResourceType, harvestAmount);

            amountUpdated?.Invoke(Amount);

            return item;
        }

        public void Regenerate()
        {
            inventory = new Inventory(new List<ItemType> { ResourceSource.ResourceType }, ResourceSource.MaxAmount);

            int amount = ResourceSource.Regenerate();
            inventory.SetItem(ResourceSource.ResourceType, amount);
            amountUpdated?.Invoke(Amount);
        }

        public void Start()
        {
            spriteRenderer.color = ResourceSource.Color;
            Regenerate();
        }

    }
}