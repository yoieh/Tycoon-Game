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
        public ItemType ResourceType;

        public override bool PrePerform(GAgent agent, GAction action)
        {
            agent.State = Worker.States.MoveTo;

            if (action.target == null)
            {
                ResourceSourceAgent _target = ResourceManager.Instance.GetClosestResourceSourceOfType(ResourceType, agent.transform.position);

                if (_target != null)
                {
                    action.target = _target.gameObject;
                }
            }

            return true;
        }

        public override bool PostPerform(GAgent agent, GAction action)
        {
            agent.State = Worker.States.Idle;

            if (action.target == null) return false;

            ResourceSourceAgent resourceSourceAgent = action.target.GetComponent<Resource.ResourceSourceAgent>();
            if (resourceSourceAgent == null) return false;

            ItemStack? harvestItemStack = resourceSourceAgent.Harvest();
            int amountRest = resourceSourceAgent.Amount;
            if (amountRest <= 0) action.target = null;

            if (harvestItemStack == null || harvestItemStack?.Amount <= 0) return false;


            Color32 color = resourceSourceAgent.ResourceSource.Color;
            // adds the item to the inventory returns false if inventory is full
            if (agent.AddItemToInventory((ItemType)ResourceType, harvestItemStack?.Amount ?? 0))
            {
                // agent.beliefs.ModifyState("Has" + ResourceType.ToString(), harvestAmount);
                agent.FeedbackText("+" + harvestItemStack?.Amount, color);

                return true;
            }

            // TODO: should drop the item on the ground if inventory is full
            return false;
        }
    }
}