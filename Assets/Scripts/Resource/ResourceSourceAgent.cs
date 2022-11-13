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
        [SerializeField] private int amount;
        public int Amount { get { return amount; } }

        public int Harvest()
        {
            if (amount <= 0)
            {
                return 0;
            }

            int harvestAmount = ResourceSource.Harvest();

            amount -= harvestAmount;
            amountUpdated?.Invoke(amount);

            return harvestAmount;
        }

        public void Regenerate()
        {
            amount = ResourceSource.Regenerate();
            amountUpdated?.Invoke(amount);
        }

        public void Start()
        {
            spriteRenderer.color = ResourceSource.Color;
            Regenerate();
        }

    }
}