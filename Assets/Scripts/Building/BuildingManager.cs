using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingManager : MonoBehaviourSingleton<BuildingManager>
{
    [SerializeField] private GameObject FloatingTextPrefab;

    // Reference to the building prefab
    [SerializeField] private GameObject buildingPrefab;
    [SerializeField] private BuildingScriptableObject buildingData;

    void FixedUpdate()
    {
        // Get the Input System's PlayerInput component.
        Mouse gamepad = Mouse.current;

        // Check if the left mouse button was pressed.
        if (gamepad.leftButton.wasPressedThisFrame)
        {
            // Get the mouse position in world space.
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            BuildBuilding(buildingData, mousePosition);
        }
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
            // Instantiate a new building at the specified location
            GameObject newBuilding = Instantiate(buildingPrefab, position, Quaternion.identity);
            newBuilding.transform.parent = transform;

            // Configure the newly created building using the data from the scriptable object
            newBuilding.name = buildingData.name;
            newBuilding.GetComponent<Building>().cost = buildingData.cost;
            // ... add any other necessary configurations here

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

    public void FeedbackText(string text, Vector2 position, Color? color = null)
    {
        GameObject prefab = Instantiate(FloatingTextPrefab, position, Quaternion.identity);
        prefab.GetComponentInChildren<TMPro.TextMeshPro>().text = text;
        prefab.GetComponentInChildren<TMPro.TextMeshPro>().color = color ?? Color.white;
    }
}