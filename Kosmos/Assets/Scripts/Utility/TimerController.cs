using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kosmos.Utility
{
    public class TimerController : MonoBehaviour
    {
        public delegate void SimpleFunc();
        public delegate void OnUpdateFunc(float time);

        public class CustomTimer
        {
            public float time;
            public float currTime;
            public SimpleFunc onTimeEnded;
            public OnUpdateFunc onUpdate;
            public bool ignore = false;

            public CustomTimer(float time, SimpleFunc onTimeEnded, OnUpdateFunc onUpdate = null)
            {
                this.time = time;
                this.onTimeEnded = onTimeEnded;
                this.onUpdate = onUpdate;
                currTime = 0;
            }

            public bool IsReady()
            {
                return currTime >= time;
            }

            public void Unset()
            {
                ignore = true;
            }
        }

        private static TimerController singleton;

        public static TimerController Singleton
        {
            get
            {
                if(singleton == null)
                {
                    singleton = FindObjectOfType<TimerController>();

                    if(singleton == null)
                    {
                        singleton = (new GameObject()).AddComponent<TimerController>();
                    }
                }

                return singleton;
            }
        }

        private List<CustomTimer> timers = new List<CustomTimer>();

        public void AddTimer(CustomTimer newTimer)
        {
            for(int i = 0; i < timers.Count; i++)
            {
                if(timers[i] == null)
                {
                    timers[i] = newTimer;
                    return;
                }
            }

            timers.Add(newTimer);
        }

        void Update()
        {
            for(int i = 0; i < timers.Count; i++)
            {
                if (timers[i] == null)
                    continue;

                timers[i].currTime += Time.deltaTime;

                if(timers[i].IsReady())
                {
                    if(!timers[i].ignore)
                        timers[i].onTimeEnded();

                    timers[i] = null;
                }
                else
                {
                    if (timers[i].onUpdate != null)
                        timers[i].onUpdate(timers[i].currTime);
                }
            }
        }
    }
}

