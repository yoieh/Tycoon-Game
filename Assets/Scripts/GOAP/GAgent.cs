using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Worker;
using System;

namespace GOAP
{
    using Stats;
    using Actions;

    public class SubGoal
    {

        // Dictionary to store our goals
        public Dictionary<WorldStateTypes, int> sGoals;
        // Bool to store if goal should be removed
        public bool remove;

        // Constructor
        public SubGoal(WorldStateTypes s, int i, bool r = true)
        {

            sGoals = new Dictionary<WorldStateTypes, int>();
            sGoals.Add(s, i);
            remove = r;
        }
    }

    public class GAgent : MonoBehaviour
    {
        public class OnStateChangedEventArgs : EventArgs
        {
            public States state;
        }
        public event EventHandler<OnStateChangedEventArgs> OnAgentStateChange; // event to notify damage

        [SerializeField] private GameObject FloatingTextPrefab;
        [SerializeField] private States _state;
        public States State
        {
            get { return _state; }
            set
            {
                OnAgentStateChange(this, new OnStateChangedEventArgs { state = value });
                _state = value;
            }
        }
        public GAgentStats agentStats;

        public List<GActionSObject> actions = new List<GActionSObject>();
        [SerializeField] public List<GAction> _actions = new List<GAction>();

        public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();
        [SerializeField] private Inventory inventory = new Inventory();
        public WorldStates beliefs = new WorldStates();

        GPlanner planner;
        Queue<GAction> actionQueue;
        public GAction currentAction;
        SubGoal currentGoal;

        public void Awake()
        {
            foreach (GActionSObject a in actions)
            {
                // create list of GGActions
                _actions.Add(new GAction(a, /*inventory,*/ beliefs));
            }
        }

        public void SetGoal(WorldStateTypes goal, int value, int priority, bool remove)
        {
            goals.Clear();
            AddGoal(goal, value, priority, remove);
        }

        public void AddGoal(WorldStateTypes goal, int value, int priority, bool remove)
        {
            SubGoal subGoal = new SubGoal(goal, value, remove);
            goals.Add(subGoal, priority);
        }

        public void ClearGoals()
        {
            goals.Clear();
        }

        public void ClearGoal(WorldStateTypes goal)
        {
            // find goal in dictionary
            SubGoal subGoal = goals.Keys.ToList().Find(g => g.sGoals.ContainsKey(goal));
            // remove goal from dictionary
            goals.Remove(subGoal);
        }

        bool invoked = false;

        void ComplateAction()
        {
            currentAction.running = false;
            currentAction.PostPerform(this);
            invoked = false;
            currentAction = null;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (currentAction != null && currentAction.running)
            {
                // check if reached destination to destination is less than 1
                if (Vector2.Distance(currentAction.destination, transform.position) < 2f)
                {
                    if (!invoked)
                    {
                        Invoke("ComplateAction", currentAction.duration);
                        invoked = true;
                    }
                }

                return;
            }

            if (planner == null || actionQueue == null)
            {
                planner = new GPlanner();

                var sortedGoals = from entry in goals orderby entry.Value descending select entry;

                // create belief from stats
                WorldStates beliefStates = agentStats.ToWorldStates();
                // merge with beliefs
                beliefStates.Merge(beliefs);

                foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
                {
                    actionQueue = planner.plan(_actions, sg.Key.sGoals, beliefStates);
                    if (actionQueue != null)
                    {
                        currentGoal = sg.Key;
                        break;
                    }
                }
            }

            if (actionQueue != null && actionQueue.Count == 0)
            {
                if (currentGoal.remove)
                {
                    goals.Remove(currentGoal);
                }
                planner = null;
            }


            if (actionQueue != null && actionQueue.Count > 0)
            {
                currentAction = actionQueue.Dequeue();
                if (currentAction.PrePerform(this))
                {
                    // if (currentAction.target == null && currentAction.targetTag != "")
                    // {
                    //     currentAction.target = GameObject.FindWithTag(currentAction.targetTag);
                    // }

                    if (currentAction.target != null)
                    {
                        currentAction.destination = currentAction.target.transform.position;
                    }

                    currentAction.running = true;
                }
                else
                {
                    actionQueue = null;
                }

            }
        }

        public void OnActionPreformad(GAction action)
        {
            int energy = 0;
            int hunger = 0;
            int water = 0;
            int happiness = 0;
            int health = 0;

            if (action.costConditions.ContainsKey(WorldStateTypes.Energy))
            {
                energy = action.costConditions[WorldStateTypes.Energy];
            }

            if (action.costConditions.ContainsKey(WorldStateTypes.Water))
            {
                water = action.costConditions[WorldStateTypes.Water];
            }

            if (action.costConditions.ContainsKey(WorldStateTypes.Food))
            {
                hunger = action.costConditions[WorldStateTypes.Food];
            }

            if (action.costConditions.ContainsKey(WorldStateTypes.Happiness))
            {
                happiness = action.costConditions[WorldStateTypes.Happiness];
            }

            if (action.costConditions.ContainsKey(WorldStateTypes.Health))
            {
                health = action.costConditions[WorldStateTypes.Health];
            }

            agentStats.PerformAction(
                energy,
                water,
                hunger,
                happiness,
                health
            );

            if (agentStats.Energy < 20)
            {
                beliefs.SetState(WorldStateTypes.IsTired, 1);
            }


            if (agentStats.Food < 20)
            {
                beliefs.SetState(WorldStateTypes.IsHungry, 1);
            }


            if (agentStats.Water < 20)
            {
                beliefs.SetState(WorldStateTypes.IsThirsty, 1);
            }


            if (agentStats.Happiness < 20)
            {
                beliefs.SetState(WorldStateTypes.IsSad, 1);
            }

        }

        public void FeedbackText(string text, Color? color = null)
        {
            GameObject prefab = Instantiate(FloatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TMPro.TextMeshPro>().text = text;
            prefab.GetComponentInChildren<TMPro.TextMeshPro>().color = color ?? Color.white;
        }

        public bool AddItemToInventory(ItemType itemType, int amount)
        {
            return inventory.SetItem(itemType, amount);
        }

        public ItemStack GetItemFromInventory(ItemType itemType, int amount)
        {
            return inventory.GetItem(itemType, -amount);
        }

        public ItemStack GetItemFromInventory(ItemType itemType)
        {
            return inventory.GetItem(itemType);
        }

    }
}