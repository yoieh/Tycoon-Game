using TMPro;
using UnityEngine;

namespace Worker
{
    // UI system to show stats
    public class WorkerUI : MonoBehaviour
    {
        [SerializeField] private WorkerAgent workerAgent;
        [SerializeField] private WorkerStats workerStats; // reference to scriptable object asset asset

        [SerializeField] private TextMeshProUGUI health;// text to display health
        [SerializeField] private TextMeshProUGUI happiness;// text to display happiness
        [SerializeField] private TextMeshProUGUI energy;// text to display energy
        [SerializeField] private TextMeshProUGUI water;// text to display water
        [SerializeField] private TextMeshProUGUI food;// text to display food
        [SerializeField] private TextMeshProUGUI workerState;// text to display name

        // subscribe to damage event of worker at awake
        private void Awake()
        {
            workerAgent.OnAgentStateChange += OnStateChange;
            workerStats.OnHealthChanged += OnHealthChanged;
            workerStats.OnHappinessChanged += OnHappinessChanged;
            workerStats.OnEnergyChanged += OnEnergyChanged;
            workerStats.OnWaterChanged += OnWaterChanged;
            workerStats.OnFoodChanged += OnFoodChanged;
        }

        private void OnStateChange(object sender, WorkerAgent.OnStateChangedEventArgs e)
        {
            if (workerState != null)
            {
                workerState.text = e.state.ToString();
            }
        }
        //change health when damage is done
        private void OnHealthChanged(object sender, WorkerStats.OnStatChangedEventArgs e)
        {
            if (health != null)
            {
                health.text = "Health: " + e.value.ToString();
            }
        }
        //change happiness when happiness is changed
        private void OnHappinessChanged(object sender, WorkerStats.OnStatChangedEventArgs e)
        {
            if (happiness != null)
            {
                happiness.text = "Happiness: " + e.value.ToString();
            }
        }
        //change energy when energy is changed
        private void OnEnergyChanged(object sender, WorkerStats.OnStatChangedEventArgs e)
        {
            if (energy != null)
            {
                energy.text = "Energy: " + e.value.ToString();
            }
        }
        //change water when water is changed
        private void OnWaterChanged(object sender, WorkerStats.OnStatChangedEventArgs e)
        {
            if (water != null)
            {
                water.text = "Water: " + e.value.ToString();
            }
        }
        //change food when food is changed
        private void OnFoodChanged(object sender, WorkerStats.OnStatChangedEventArgs e)
        {
            if (food != null)
            {
                food.text = "Food: " + e.value.ToString();
            }
        }

        // unsubscribe to damage event of worker at destroy
        private void OnDestroy()
        {
            workerAgent.OnAgentStateChange -= OnStateChange;
            workerStats.OnHealthChanged -= OnHealthChanged;
            workerStats.OnHappinessChanged -= OnHappinessChanged;
            workerStats.OnEnergyChanged -= OnEnergyChanged;
            workerStats.OnWaterChanged -= OnWaterChanged;
            workerStats.OnFoodChanged -= OnFoodChanged;
        }
    }
}
