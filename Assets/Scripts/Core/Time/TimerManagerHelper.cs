using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SFramework.Core
{
    public partial class TimerManager
    {
       
        /// <summary>
        /// 注册一个计时器
        /// </summary>
        /// <param name="duration">计时器计时时间</param>
        /// <param name="onComplete">当计时器每次完成时</param>
        /// <param name="onUpdate">当计时器执行时，可为空</param>
        /// <param name="isLooped">是否循环执行</param>
        /// <param name="usesRealTime">是否使用真实时间，即不受time.timeScale影响的时间</param>
        /// <param name="autoDestroyOwner">拥有者</param>
        /// <returns></returns>
        public Timer RegisterTimer(float duration, Action onComplete, Action<float> onUpdate,
            bool isLooped, bool usesRealTime, object autoDestroyOwner)
        {
            Timer timer =  ReferencePool.Acquire<Timer>();
            timer.AttachTimer(duration,onComplete,onUpdate,isLooped,usesRealTime,autoDestroyOwner);
            m_Timers.Add(timer);
            return timer;
        }

        
        
        
        /// <summary>
        /// 回收计时器 内部使用，不信任外部使用，仅在内部进行轮询释放
        /// </summary>
        /// <param name="timer"></param>
        private void ReleaseTimer(Timer timer)
        {
            if (timer == null)
            {
                throw new SException("Timer is Null");
            }
            ReferencePool.Release(timer);
            if (!m_Timers.Contains(timer))
            {
                throw new SException("不受管理的timer计时器，底层代码有问题!!!!!!!");
            }
            m_Timers.Remove(timer);
        }


        /// <summary>
        /// 释放已经使用完成的计时器
        /// </summary>
        public void ReleaseIsDoneTimer()
        {
            for (int i = 0; i < m_Timers.Count; i++)
            {
                Timer timer = m_Timers[i];
                if (timer.IsDone)
                {
                    ReleaseTimer(timer);
                }
            }
            
            
        }

        /// <summary>
        /// 暂停所有计时器
        /// </summary>
        public void PauseAllTimer()
        {
            foreach (var timer in m_Timers)
            {
                timer.Pause();
            }
        }

        /// <summary>
        /// 取消所有计时器
        /// </summary>
        public void CancelAllTimer()
        {
            foreach (var timer in m_Timers)
            {
                timer.Cancel();
            }
        }

        /// <summary>
        /// 重启所有计时器
        /// </summary>
        public void ResumeAllTimer()
        {
            foreach (var timer in m_Timers)
            {
                timer.Resume();
            }
        }

        /// <summary>
        /// 重置所有计时器
        /// </summary>
        public void ResetAllTimer()
        {
            foreach (var timer in m_Timers)
            {
                timer.Reset();
            }
        }
    }
}

