using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resource;

namespace GOAP.Actions
{
    [CreateAssetMenu(fileName = "DeliverResourceNAME", menuName = "Actoins/DeliverResource")]
    public class DeliverResource : GActionSObject
    {
        public ItemType ResourceType;

        public override bool PrePerform(GAgent agent, GAction action)
        {
            Building building = BuildingManager.Instance.GetClosestBuildingOfType(BuildingType.Storage, agent.transform.position);
            
            Debug.Log("building: " + building);

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

            bool hasItem = agent.HasItemInInventory(ResourceType);

            if (hasItem)
            {
                ItemStack? itemStack = agent.GetItemFromInventory(ResourceType);

                // agent.beliefs.ModifyState("Has" + ResourceType.ToString(), -1);

                WorldStateTypes stateType = WorldState.ByName("Delivered" + ResourceType.ToString());

                // TODO: add inventory to deliverd storage
                GWorld.Instance.GetWorld().ModifyState(stateType, itemStack?.Amount ?? 0);
                GlobalStorage.AddItemToInventory(ResourceType, itemStack?.Amount ?? 0);

                agent.FeedbackText("+" + itemStack?.Amount);

                return true;
            }

            return false;
        }
    }
}