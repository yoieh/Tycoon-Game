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
            Building building = BuildingManager.Instance.GetClosestBuildingOfType(BuildingType.Storage, agent.transform.position);

            if (building == null)
            {
                return false;
            }

            agent.State = Worker.States.MoveTo;

            action.target = building.gameObject;

            return true;
        }

        public override bool PostPerform(GAgent agent, GAction action)
        {
            agent.State = Worker.States.Idle;

            ItemStack? itemStack = GlobalStorage.Inventory.GetItem(ItemType.Food, action.costConditions[WorldStateTypes.Food]);

            if (itemStack == null || itemStack?.Amount < action.costConditions[WorldStateTypes.Food])
            {
                agent.FeedbackText("No food");
                return false;
            }

            agent.FeedbackText(WorldStateTypes.HasEaten.ToString());
            agent.beliefs.ModifyState(WorldStateTypes.IsHungry, -1);

            return true;
        }
    }
}