using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resource;

namespace GOAP.Actions
{
    [CreateAssetMenu(fileName = "GoAndRest", menuName = "Actoins/GoAndRest")]
    public class GoAndRest : GActionSObject
    {
        public override bool PrePerform(GAgent agent, GAction action)
        {
            Building building = BuildingManager.Instance.GetClosestBuildingOfType(BuildingType.Storage, agent.transform.position);

            agent.State = Worker.States.MoveTo;

            if (building == null)
            {
                action.target = agent.gameObject;

                return true;
            }


            action.target = building.gameObject;

            return true;
        }

        public override bool PostPerform(GAgent agent, GAction action)
        {
            agent.State = Worker.States.Idle;

            agent.FeedbackText(WorldStateTypes.HasRested.ToString());
            agent.beliefs.ModifyState(WorldStateTypes.IsTired, -1);

            return true;
        }
    }
}