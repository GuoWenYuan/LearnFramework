using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework.Core
{
    public interface ILifeBehaviour
    {
        /// <summary>
        /// 优先级，便利时根据优先级进行遍历,优先级逻辑同deapth 越低越靠前执行
        /// </summary>
        int Priority { get; set; }


        /// <summary>
        /// Instance创建时的ID
        /// </summary>
        int InstanceId { get; }

        /// <summary>
        /// 当初始化
        /// </summary>
        void OnInit();

        /// <summary>
        /// 当开始
        /// </summary>
        void OnStart();
        /// <summary>
        /// update逻辑
        /// </summary>
        void OnUpdate(float elapseSeconds, float realElapseSeconds);
        
        /// <summary>
        /// 当停止时
        /// </summary>
        void ShutDown();
        
        
    }
    

}


