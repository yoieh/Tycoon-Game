using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Systems
{
    interface ITickedSystem
    {
        void OnAwake();
        void WillTickUpdate();
        void TickUpdate();
        void HasTickUpdated();
    }

    public class BaseTickedSystem : MonoBehaviour, ITickedSystem
    {
        public virtual void OnAwake() { }
        public virtual void WillTickUpdate() { }
        public virtual void TickUpdate() { }
        public virtual void HasTickUpdated() { }

        public void Awake()
        {
            OnAwake();
            TimeTickSystem.OnTick += TimeTickSystem_OnTick;
        }

        private void TimeTickSystem_OnTick(object sender, TimeTickSystem.OnTickEventArgs e)
        {
            WillTickUpdate();
            TickUpdate();
            HasTickUpdated();
        }
    }



}