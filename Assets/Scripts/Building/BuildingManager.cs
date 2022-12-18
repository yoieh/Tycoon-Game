using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public enum BuildingType
{
    House,
    Storage
}

public class BuildingManager : MonoBehaviourSingleton<BuildingManager>
{
    [SerializeField] private GameObject FloatingTextPrefab;

    // Reference to the building prefab
    [SerializeField] private BuildingScriptableObject buildingData;


    private List<Building> Buildings = new List<Building>();


    public void Awake()
    {
        // Add storage building to the center of the map
        SpawnBuilding(buildingData, Vector2.zero);
    }

    private void Start()
    {
        // Get the Input System's PlayerInput component.
        PlayerInput playerInput = GetComponent<PlayerInput>();

        // Register the OnActionPreformad method to the Fire action.
        playerInput.actions["Fire"].performed += OnActionPreformad;
    }


    // Method for instantiating a new building
    public void BuildBuilding(BuildingScriptableObject buildingData, Vector2 position)
    {
        string text = "";
        Color color = Color.red;

        bool canBuild = true;

        foreach (ItemStack cost in buildingData.cost)
        {
            // Check if the player has enough resources to build the building
            bool hasItem = GlobalStorage.HasItemInInventory(cost.ItemType, cost.Amount);
            if (!hasItem)
            {
                canBuild = false;
                break;
            }
        }

        if (canBuild)
        {
            SpawnBuilding(buildingData, position);

            text = $"{buildingData.name} built \n";
            // Deduct the cost of the building from the player's inventory
            foreach (ItemStack cost in buildingData.cost)
            {
                // GlobalStorage.AddItemToInventory(cost.ItemType, -cost.Amount);
                GlobalStorage.GetItemFromInventory(cost.ItemType, cost.Amount);

                // Display a feedback text indicating the cost of the building
                text += $" -{cost.Amount} {cost.ItemType} \n";
            }

            color = Color.green;
        }
        else
        {
            // Display a message indicating that the player does not have enough resources to build the building
            text = "Not enough resources to build building";
        }

        FeedbackText(text, position, color);
    }

    public Building GetClosestBuildingOfType(BuildingType buildingType, Vector2 position)
    {
        Building closest = null;

        Debug.Log("Buildings: " + Buildings.Count);

        // Get all buildings of the specified type
        foreach (Building building in Buildings)
        {
            Debug.Log("BuildingType: " + building.BuildingType);

            if (building.BuildingType == buildingType)
            {

                if (closest == null)
                {
                    closest = building;
                }
                else
                {
                    // Check if the current building is closer than the previously found closest building
                    if (Vector2.Distance(position, building.transform.position) < Vector2.Distance(position, closest.transform.position))
                    {
                        closest = building;
                    }
                }
            }
        }

        return closest;
    }

    private Building SpawnBuilding(BuildingScriptableObject buildingData, Vector2 position)
    {
        // Instantiate a new building at the specified location
        GameObject newBuilding = Instantiate(buildingData.prefab, position, Quaternion.identity);
        newBuilding.transform.parent = transform;

        // Configure the newly created building using the data from the scriptable object
        newBuilding.name = buildingData.name;
        newBuilding.GetComponent<Building>().cost = buildingData.cost;
        newBuilding.GetComponent<Building>().BuildingType = buildingData.buildingType;

        // Add the building to the list of buildings
        Buildings.Add(newBuilding.GetComponent<Building>());

        return newBuilding.GetComponent<Building>();
    }

    private void OnActionPreformad(InputAction.CallbackContext context)
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        // Get the mouse position in world space.
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        BuildBuilding(buildingData, mousePosition);
    }

    private void FeedbackText(string text, Vector2 position, Color? color = null)
    {
        GameObject prefab = Instantiate(FloatingTextPrefab, position, Quaternion.identity);
        prefab.GetComponentInChildren<TMPro.TextMeshPro>().text = text;
        prefab.GetComponentInChildren<TMPro.TextMeshPro>().color = color ?? Color.white;
    }
}