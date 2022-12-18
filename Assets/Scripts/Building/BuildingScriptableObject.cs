using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class BuildingScriptableObject : ScriptableObject
{
    // Name of the building
    public string Name;

    // Cost of the building
    public List<ItemStack> cost = new List<ItemStack>();

    // Position where the building should be spawned
    public Vector3 spawnPosition;

    // Any other properties you want to define for your building
    // ...
}