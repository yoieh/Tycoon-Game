using GOAP;

// Actions
// 0 - None
// 1 - Move
// 2 - Harvest
// 3 - Build
// 4 - Repair
// 5 - Upgrade
// 6 - Attack
// 7 - Flee
// 8 - Consume
// 9 - Die

namespace Worker
{

    public class WorkerAgent : GAgent
    {
        public float Speed = 1f;

        public void Start()
        {
            agentStats = GetComponent<GAgentStats>();
            agentStats.TiggerAllStatsEvents();
        }

        // deal damage to worker
        public void WorkerHit()
        {
            agentStats.HealthDecrease(1);
        }
    }
}
