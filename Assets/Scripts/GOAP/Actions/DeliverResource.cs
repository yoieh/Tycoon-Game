using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resource;

namespace GOAP.Actions
{
    [CreateAssetMenu(fileName = "DeliverResourceNAME", menuName = "Actoins/DeliverResource")]
    public class DeliverResource : GActionSObject
    {
        public ResourceType ResourceType;

        public override bool PrePerform(GAgent agent, GAction action)
        {
            agent.State = Worker.States.MoveTo;

            // action.target = find storage building

            return true;
        }

        public override bool PostPerform(GAgent agent, GAction action)
        {
            agent.State = Worker.States.Idle;

            ItemStack itemStack = agent.GetItemFromInventory((ItemType)ResourceType);

            if (itemStack != null)
            {
                // agent.beliefs.ModifyState("Has" + ResourceType.ToString(), -1);
                GWorld.Instance.GetWorld().ModifyState("Stored" + ResourceType.ToString(), itemStack.Amount);

                agent.FeedbackText("+" + itemStack.Amount);

                return true;
            }

            return false;
        }
    }
}