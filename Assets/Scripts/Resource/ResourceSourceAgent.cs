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

        public void Harvest()
        {
            amount = ResourceSource.Harvest();
            amountUpdated?.Invoke(amount);
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