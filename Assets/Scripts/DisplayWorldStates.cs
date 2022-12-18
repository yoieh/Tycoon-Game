using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GOAP;

public class DisplayWorldStates : MonoBehaviour
{

    public TextMeshProUGUI text;
    // Update is called once per frame
    void LateUpdate()
    {
        string s = "";
        foreach (KeyValuePair<ItemType, ItemStack> w in GlobalStorage.Inventory.ItemsSlots)
        {
            s += w.Key + ": " + w.Value.Amount + "\n";
        }

        // foreach (KeyValuePair<WorldStateTypes, int> w in GWorld.Instance.GetWorld().GetStates())
        // {
        //     s += w.Key + ": " + w.Value + "\n";
        // }

        text.text = s;
    }
}
