using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resource;
using System.Linq;

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

        agent.beliefs.ModifyState("Has" + ResourceType.ToString(), 1);

        return true;
    }
}
