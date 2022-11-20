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
            agent.State = Worker.States.MoveTo;

            action.target = agent.gameObject;

            return true;
        }

        public override bool PostPerform(GAgent agent, GAction action)
        {
            agent.State = Worker.States.Idle;

            agent.FeedbackText(WorldStateTypes.HasDrank.ToString());
            agent.beliefs.ModifyState(WorldStateTypes.IsThirsty, -1);

            return true;
        }
    }
}