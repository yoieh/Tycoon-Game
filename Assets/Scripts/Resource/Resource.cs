using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Resource
{
    [CreateAssetMenu(fileName = "ResourceName", menuName = "Resource")]
    public class Resource : ScriptableObject
    {
        public ResourceType ResourceType;
        public Color32 Color = new Color32(255, 255, 255, 255);
        public int MinHarvestAmount = 1;
        public int MaxHarvestAmount = 1;
        public float HarvestTime = 3f;
        public float RegenerationTime = 10f;
        public int MaxAmount = 100;
        public int MinAmount = 1;

        public int Harvest()
        {
            return Random.Range(MinHarvestAmount, MaxHarvestAmount);
        }

        public int Regenerate()
        {
            return Random.Range(MinAmount, MaxAmount);
        }
    }
}