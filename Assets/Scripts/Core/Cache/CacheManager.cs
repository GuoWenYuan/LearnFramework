using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework.Core
{
    /// <summary>
    /// 用于管理所有的缓存池
    /// </summary>
    public class CacheManager : SGameManager
    {
        /// <summary>
        /// 降低缓冲池优先级
        /// </summary>
        public override int Priority => 8;

        public override void OnInit()
        {
           
        }

        public override void OnStart()
        {
            //后续用配置文件
            ReferencePool.OnStart(false,10);
        }

        public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            ReferencePool.OnUpdate(elapseSeconds,realElapseSeconds);
        }

        public override void ShutDown()
        {
            ReferencePool.ClearAll();
        }
    }
}

