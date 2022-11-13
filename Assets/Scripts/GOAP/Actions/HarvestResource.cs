using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resource;
using System.Linq;

namespace GOAP.Actions
{
    [CreateAssetMenu(fileName = "HarvestResourceNAME", menuName = "Actoins/HarvestResource")]
    public class HarvestResource : GActionSObject
    {
        public ResourceType ResourceType;

        public override bool PrePerform(GAgent agent, GAction action)
        {
            agent.State = Worker.States.MoveTo;

            if (action.target == null)
            {
                action.target = ResourceManager.Instance.GetClosestResourceSourceOfType(ResourceType, agent.transform.position).gameObject;
            }

            return true;
        }

        public override bool PostPerform(GAgent agent, GAction action)
        {
            agent.State = Worker.States.Idle;

            int harvestAmount = action.target.GetComponent<Resource.ResourceSourceAgent>().Harvest();
            int amount = action.target.GetComponent<Resource.ResourceSourceAgent>().Amount;
            Color32 color = action.target.GetComponent<Resource.ResourceSourceAgent>().ResourceSource.Color;

            if (amount <= 0)
            {
                action.target = null;
            }

            // failed to harvest
            if (harvestAmount <= 0)
            {
                return false;
            }

            // adds the item to the inventory returns false if inventory is full
            if (agent.AddItemToInventory((ItemType)ResourceType, harvestAmount))
            {
                // agent.beliefs.ModifyState("Has" + ResourceType.ToString(), harvestAmount);
                agent.FeedbackText("+" + harvestAmount, color);

                return true;
            }

            // TODO: should drop the item on the ground if inventory is full
            return false;
        }
    }
}