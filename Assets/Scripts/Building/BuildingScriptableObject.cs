using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building", menuName = "Building")]
public class BuildingScriptableObject : ScriptableObject
{
    // Name of the building
    public string Name;

    // Cost of the building
    public List<ItemStack> cost = new List<ItemStack>();

    public BuildingType buildingType;

    public GameObject prefab;

    // Any other properties you want to define for your building
    // ...
}