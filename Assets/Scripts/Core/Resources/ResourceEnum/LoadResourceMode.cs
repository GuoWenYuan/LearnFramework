﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFramework.Core
{
    public enum LoadResourceMode : byte
    {

        None = 0,
        /// <summary>
        /// 异步
        /// </summary>
        Aysnc,

        /// <summary>
        /// 同步
        /// </summary>
        UnAysnc,



    }
}
