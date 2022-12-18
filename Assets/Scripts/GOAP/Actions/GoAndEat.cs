using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resource;

namespace GOAP.Actions
{
    [CreateAssetMenu(fileName = "GoAndEat", menuName = "Actoins/GoAndEat")]
    public class GoAndEat : GActionSObject
    {
        public override bool PrePerform(GAgent agent, GAction action)
        {
            agent.State = Worker.States.MoveTo;

            action.target = agent.gameObject;

            return true;
        }

        public override bool PostPerform(GAgent agent, GAction action)
        {
            agent.State = Worker.States.Idle;

            GlobalStorage.Inventory.GetItem(ItemType.Food, -1);

            agent.FeedbackText(WorldStateTypes.HasEaten.ToString());
            agent.beliefs.ModifyState(WorldStateTypes.IsHungry, -1);

            return true;
        }
    }
}