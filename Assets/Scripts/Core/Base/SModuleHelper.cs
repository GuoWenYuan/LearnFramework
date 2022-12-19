using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework.Core
{
    public static class SModuleHelper
    {
        private static int sCeaterId = 100000;

        /// <summary>
        /// 创建InstanceId
        /// </summary>
        /// <returns></returns>
        public static int CreateInstanceId()
        {
            return sCeaterId++;
        }
        

        
      
    }
}

