using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework.Core
{
    /// <summary>
    /// 用于计时的时间管理器
    /// </summary>
    public partial class Timer : IReference , IUpdate
    {
        /// <summary>
        /// 需要进行计时的时间
        /// </summary>
        public float duration;

        /// <summary>
        /// 是否开始计时
        /// </summary>
        public bool isLooped;

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool isCompleted;

        /// <summary>
        /// 使用的真实时间
        /// </summary>
        public bool useRealTime;

        
        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool IsPause
        {
            get { return this.m_TimeElapsedBeforPause.HasValue; }
            
        }

        /// <summary>
        /// 是否取消
        /// </summary>
        public bool IsCancelled
        {
            get { return this.m_TimeElapsedBeforCanel.HasValue; }
            
        }
        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsDone
        {
            get { return this.isCompleted || this.IsCancelled || this.IsOwnerDestroyed; }

        }

        /// <summary>
        /// 当前计时开始时间
        /// </summary>
        private float m_StartTime;

        
        /// <summary>
        /// 当前最后一次的update时间
        /// </summary>
        private float m_LastUpdateTime;
        /// <summary>
        /// 取消的时间
        /// </summary>
        private float? m_TimeElapsedBeforCanel;

        /// <summary>
        /// 暂停的时间
        /// </summary>
        private float? m_TimeElapsedBeforPause;
       

        /// <summary>
        /// 当完成时
        /// </summary>
        private Action m_OnCompleted;
        /// <summary>
        /// 当update时
        /// </summary>
        private Action<float> m_OnUpdate;


        
        /// <summary>
        /// 拥有者
        /// </summary>
        private object m_AutoDestroyOwner;

        /// <summary>
        /// 当前拥有者是否被销毁
        /// </summary>
        private bool m_HasAutoDestroyOwner;


        /// <summary>
        /// 拥有者是否销毁
        /// </summary>
        private bool IsOwnerDestroyed
        {
            get { return m_HasAutoDestroyOwner && m_AutoDestroyOwner == null; }

        }
        
        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (IsDone)
            {
                return;;
            }

            if (IsPause)
            {
                m_StartTime += this.GetTimeDelta();
                this.m_LastUpdateTime = this.GetWorldTime();
                return;;
            }

            m_OnUpdate?.Invoke(GetTimeElapsed());

            //如果当前真实时间大于该计时器需要执行的时间
            if (GetWorldTime() > GetFireTime())
            {
                m_OnCompleted?.Invoke();
                if (isLooped)
                {
                    m_StartTime = GetWorldTime();
                }
                else
                {
                    this.isCompleted = true;
                }
                
            }
        }
  
        /// <summary>
        /// 销毁
        /// </summary>
        public void Clear()
        {
            m_TimeElapsedBeforCanel = null;
            m_TimeElapsedBeforPause = null;
            m_OnCompleted = null;
            m_OnUpdate = null;
            m_AutoDestroyOwner = null;
            m_HasAutoDestroyOwner = false;
        }

        public float LastUseTime { get; set; }
    }
}

