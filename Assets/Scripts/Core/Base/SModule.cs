using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SFramework.Core
{
    public abstract class SGameManager : ILifeBehaviour
    {
        public virtual int Priority { get; set; }
        public int InstanceId { get; }

        protected SGameManager()
        {
            InstanceId = SModuleHelper.CreateInstanceId();
        }

        protected SGameManager(int priority)
        {
            InstanceId = SModuleHelper.CreateInstanceId();
            Priority = priority;
        }

        public virtual void OnInit()
        {
            
        }

        public virtual void OnStart()
        {
             
        }

        public virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
           
        }

        public virtual void ShutDown()
        {
            
        }
    }
}

