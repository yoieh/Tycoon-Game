using System;
using UnityEngine;

namespace Systems
{
    public class TimeTickSystem : MonoBehaviour
    {
        public class OnTickEventArgs : EventArgs
        {
            public int tick;
        }

        public static event EventHandler<OnTickEventArgs> OnTick;

        private const float TICK_TIMER_MAX = .2f;

        private int tick;
        private float tickTimer = 0f;

        private float _timeScale = 1f;

        private void Awake()
        {
            Time.timeScale = 1;
            tick = 0;
        }

        private void Update()
        {
            tickTimer += Time.deltaTime;
            if (tickTimer >= TICK_TIMER_MAX)
            {
                tickTimer = 0f;
                tick++;
                if (OnTick != null) OnTick(this, new OnTickEventArgs { tick = tick });
            }
        }

        public void PauseToggle()
        {
            if (Time.timeScale <= 0)
            {
                SetTimeScale(_timeScale);
            }
            else
            {
                Time.timeScale = 0f;
            }
        }

        public void SetTimeScale(float timeScale)
        {
            _timeScale = timeScale;
            Time.timeScale = _timeScale;
        }
    }
}