using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Worker
{
    public enum States
    {
        Idle,
        MoveTo,
        PerformAction,
        Dead
    }

    public enum Needs
    {
        Food,
        Water,
        Sleep,
        Health,
        Happiness
    }


    public class WorkerManager : MonoBehaviourSingleton<WorkerManager>
    {
        public Queue<WorkerAgent> WorkersQueue = new Queue<WorkerAgent>();

        [SerializeField] private int spawnAmount = 10;
        [SerializeField] private GameObject workerPrefab;

        public void AddWorker(WorkerAgent worker)
        {
            WorkersQueue.Enqueue(worker);
        }

        public WorkerAgent RemoveWorker()
        {
            if (WorkersQueue.Count == 0) return null;
            return WorkersQueue.Dequeue();
        }

        public Queue<WorkerAgent> FindAllWithState(States state)
        {
            Queue<WorkerAgent> workers = new Queue<WorkerAgent>();

            foreach (WorkerAgent worker in WorkersQueue)
            {
                if (worker.State == state)
                {
                    workers.Enqueue(worker);
                }
            }

            return workers;
        }

        public void Start()
        {
            // create workers
            for (int i = 0; i < spawnAmount; i++)
            {
                GameObject worker = Instantiate(workerPrefab) as GameObject;
                worker.transform.parent = transform;

                worker.transform.position = new Vector2(UnityEngine.Random.Range(-50, 50), UnityEngine.Random.Range(-25, 25));
                worker.name = "Worker " + i;

                WorkerAgent workerAgent = worker.GetComponent<WorkerAgent>();

                // get random resource to harvest
                var values = Enum.GetValues(typeof(Resource.ResourceType));
                Resource.ResourceType pickedResource = (Resource.ResourceType)UnityEngine.Random.Range(0, values.Length);

                workerAgent.SetGoal("Delivered" + pickedResource.ToString(), 1, 3, false);


                AddWorker(workerAgent);
            }
        }

    }
}