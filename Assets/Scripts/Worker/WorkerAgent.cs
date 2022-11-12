using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private WorkerStats workerStats; // reference to scriptable object asset asset

        public void Start()
        {
            workerStats = GetComponent<WorkerStats>();
        }

        // deal damage to worker
        public void WorkerHit()
        {
            workerStats.Damage(1);
        }
    }
}
