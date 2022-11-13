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

            if (harvestAmount <= 0)
            {
                return false;
            }

            agent.beliefs.ModifyState("Has" + ResourceType.ToString(), harvestAmount);
            agent.FeedbackText("+" + harvestAmount, color);

            return true;
        }
    }
}