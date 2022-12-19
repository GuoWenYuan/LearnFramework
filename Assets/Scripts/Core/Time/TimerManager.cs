using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SFramework.Core
{
    public partial class TimerManager : SGameManager
    {
        public override int Priority => 12;

        private List<Timer> m_Timers;

        private SUnityTime m_UnityTime;

        public SUnityTime UnityTime => m_UnityTime;
        public override void OnInit()
        {
            m_Timers = new List<Timer>();
            m_UnityTime = new SUnityTime();
        }

        public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            foreach (var timer in m_Timers)
            {
                timer.OnUpdate(elapseSeconds,realElapseSeconds);
            }

            ReleaseIsDoneTimer();
        }

        public override void ShutDown()
        {
            m_Timers.Clear();
        }
    }
}

