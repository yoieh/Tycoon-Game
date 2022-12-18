using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resource;

namespace GOAP.Actions
{
    [CreateAssetMenu(fileName = "GoAndDrink", menuName = "Actoins/GoAndDrink")]
    public class GoAndDrink : GActionSObject
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

            ItemStack? itemStack = GlobalStorage.Inventory.GetItem(ItemType.Water, action.costConditions[WorldStateTypes.Water]);

            if (itemStack == null)
            {
                agent.FeedbackText("No water");
                return false;
            }

            agent.FeedbackText(WorldStateTypes.HasDrank.ToString());
            agent.beliefs.ModifyState(WorldStateTypes.IsThirsty, -1);

            return true;
        }
    }
}