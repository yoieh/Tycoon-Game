using TMPro;
using UnityEngine;


namespace Resource
{
    public class ResourceSourceUI : MonoBehaviour
    {
        [SerializeField] private ResourceSourceAgent resourceSourceAgent;
        [SerializeField] private TextMeshProUGUI resourceAmount;

        private void Awake()
        {
            resourceSourceAgent.amountUpdated += OnAmountUpdated;
        }

        private void OnAmountUpdated(int amount)
        {
            resourceAmount.text = amount.ToString();
        }

    }
}