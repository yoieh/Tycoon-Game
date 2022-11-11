using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;

namespace Worker.Systems
{
    public class MovementSystem : BaseTickedSystem
    {
        // Movment should not be ticked :D
        public void Update()
        {
            // get all workers
            Queue<WorkerAgent> workers = WorkerManager.Instance.FindAllWithState(States.MoveTo);

            // loop through workers
            foreach (WorkerAgent worker in workers)
            {
                if (worker.currentAction == null) continue;
                if (!worker.currentAction.running) continue;

                // get worker's transform
                Transform workerTransform = worker.transform;

                // get worker's destination
                Vector3 destination = worker.currentAction.destination;

                // get worker's speed
                float speed = worker.Speed;

                // move worker towards destination
                workerTransform.position = Vector3.MoveTowards(workerTransform.position, destination, speed * Time.deltaTime);

                // if worker has reached destination
                if (workerTransform.position == destination)
                {
                    // set worker's state to preform action
                    worker.State = States.PerformAction;
                }
            }
        }
    }
}