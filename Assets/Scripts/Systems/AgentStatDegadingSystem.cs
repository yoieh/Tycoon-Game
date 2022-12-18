using System;
using System.Collections;
using System.Collections.Generic;
using GOAP.Stats;
using Systems;
using UnityEngine;
using Worker;

public class AgentStatDegadingSystem : BaseTickedSystem
{
    Queue<WorkerAgent> workersToUpdate;

    int tick = 0;
    int tickRate = 10; // the rate of ticks neeaded to pass before stats are decreased

    public override void WillTickUpdate()
    {
        workersToUpdate = new Queue<WorkerAgent>();

        // if tick is not 0
        if (tick != 0)
        {
            // decrease tick
            tick--;
            return;
        }

        // reset tick
        tick = tickRate;

        // get all workers to update
        workersToUpdate = new Queue<WorkerAgent>(WorkerManager.Instance.WorkersQueue);
    }

    public override void TickUpdate()
    {
        foreach (WorkerAgent worker in workersToUpdate)
        {
            int energy = -1;
            int food = -1;
            int water = -1;

            int happiness = 0;
            int health = 0;

            if (worker.agentStats.Energy <= 0 || worker.agentStats.Food <= 0 || worker.agentStats.Water <= 0)
            {
                happiness = -1;
                health = -1;
            }

            if (worker.agentStats.Energy <= 0)
            {
                energy = 0;
            }

            if (worker.agentStats.Food <= 0)
            {
                food = 0;
            }

            if (worker.agentStats.Water <= 0)
            {
                water = 0;
            }

            if (worker.agentStats.Happiness <= 0)
            {
                happiness = 0;
            }

            if (worker.agentStats.Health <= 0)
            {
                health = 0;
            }

            worker.agentStats.PerformAction(
                energy,
                food,
                water,
                health,
                happiness
           );

            // update beliefs
            UpdateBeliefs(worker);
        }

    }

    private void UpdateBeliefs(WorkerAgent worker)
    {
        if (worker.agentStats.Energy < 20)
        {
            worker.beliefs.SetState(WorldStateTypes.IsTired, 1);
        }


        if (worker.agentStats.Food < 20)
        {
            worker.beliefs.SetState(WorldStateTypes.IsHungry, 1);
        }


        if (worker.agentStats.Water < 20)
        {
            worker.beliefs.SetState(WorldStateTypes.IsThirsty, 1);
        }


        if (worker.agentStats.Happiness < 20)
        {
            worker.beliefs.SetState(WorldStateTypes.IsSad, 1);
        }
    }
}
