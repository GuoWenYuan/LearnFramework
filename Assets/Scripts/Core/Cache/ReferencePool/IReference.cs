using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework.Core
{
    public interface IReference 
    {
        /// <summary>
        /// 清理引用。
        /// </summary>
        void Clear();

        /// <summary>
        /// 未使用时间
        /// </summary>
        float LastUseTime { get; set; }
    }
}

