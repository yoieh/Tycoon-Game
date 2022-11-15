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
            agent.State = Worker.States.MoveTo;

            action.target = agent.gameObject;

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