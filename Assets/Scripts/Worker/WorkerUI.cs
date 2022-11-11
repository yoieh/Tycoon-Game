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
        [SerializeField] private TextMeshProUGUI workerState;// text to display name

        private void Update()
        {
            health.text = workerStats.GetHealth().ToString();
        }

        // subscribe to damage event of worker at awake
        private void Awake()
        {
            workerAgent.agentStateChange += OnStateChange;
            workerStats.workerDamaged += OnHealthChanged;
        }

        //change health when damage is done
        private void OnHealthChanged(float currentHealth)
        {
            health.text = currentHealth.ToString();
        }

        private void OnStateChange(States currentState)
        {
            workerState.text = currentState.ToString();
        }
    }
}
