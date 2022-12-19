using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SFramework.Core
{
    public partial class Timer
    {
        /// <summary>
        /// 进行计时操作，不在构造函数中进行行为，Timer会进ReferencePool中进行管理
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="onComplete"></param>
        /// <param name="onUpdate"></param>
        /// <param name="isLooped"></param>
        /// <param name="usesRealTime"></param>
        /// <param name="autoDestroyOwner"></param>
        public void AttachTimer(float duration, Action onComplete, Action<float> onUpdate,
            bool isLooped, bool usesRealTime, object autoDestroyOwner)
        {
            this.duration = duration;
            this.m_OnCompleted = onComplete;
            this.m_OnUpdate = onUpdate;
            this.isLooped = isLooped;

            this.useRealTime = usesRealTime;
            
            this.m_AutoDestroyOwner = autoDestroyOwner;
            this.m_HasAutoDestroyOwner = m_AutoDestroyOwner != null;

            this.m_StartTime = GetWorldTime();
            this.m_LastUpdateTime = this.m_StartTime;
        }


        /// <summary>
        /// 获取当前游戏进行时间
        /// </summary>
        /// <returns></returns>
        private float GetWorldTime()
        {
            return useRealTime ? Time.realtimeSinceStartup : Time.time;
        }

        /// <summary>
        /// 获取执行总时间
        /// </summary>
        /// <returns></returns>
        private float GetFireTime()
        {
            return m_StartTime + duration;
        }

        /// <summary>
        /// 类同于Unity Time.DeltaTime
        /// </summary>
        /// <returns></returns>
        private float GetTimeDelta()
        {
            return this.GetWorldTime() - this.m_LastUpdateTime;
        }

        /// <summary>
        /// 获取次计时器执行的时间
        /// </summary>
        /// <returns></returns>
        public float GetTimeElapsed()
        {
            if (this.isCompleted || this.GetWorldTime() >= GetFireTime())
            {
                return this.duration;
            }

            return this.m_TimeElapsedBeforCanel ??
                   this.m_TimeElapsedBeforPause ?? this.GetWorldTime() - this.m_StartTime;
        }
        

        /// <summary>
        /// 取消
        /// </summary>
        public void Cancel()
        {
            if (this.IsDone)
            {
                return;;
            }

            this.m_TimeElapsedBeforCanel = GetTimeElapsed();
            this.m_TimeElapsedBeforPause = null;
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            if (this.IsPause || this.IsDone)
            {
                return;
            }

            this.m_TimeElapsedBeforPause = GetTimeElapsed();
            
        }

        /// <summary>
        /// 继续执行
        /// </summary>
        public void Resume()
        {
            if (!this.IsPause || this.IsDone)
            {
                return;
            }

            this.m_TimeElapsedBeforPause = null;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            Resume();
            m_StartTime = GetWorldTime();
            m_LastUpdateTime = m_StartTime;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="duration"></param>
        public void Reset(float duration)
        {
            Resume();
            this.duration = duration;
            m_StartTime = GetWorldTime();
            m_LastUpdateTime = m_StartTime;
        }

        /// <summary>
        /// 获取当前还有多长时间执行完成
        /// </summary>
        /// <returns></returns>
        public float GetTimeRemaining()
        {
            return duration - GetTimeElapsed();
        }

        /// <summary>
        /// 获取完成比例
        /// </summary>
        /// <returns></returns>
        public float GetRatioComplete()
        {
            return GetTimeElapsed() / duration;
        }

        /// <summary>
        /// 获取剩余完成比例
        /// </summary>
        /// <returns></returns>
        public float GetRatioRemaining()
        {
            return GetTimeRemaining() / duration;
        }

    }
}