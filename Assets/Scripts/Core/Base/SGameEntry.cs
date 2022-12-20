using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework.Core
{
    public static partial class SGameEntry 
    {
        /// <summary>
        /// Log管理器
        /// </summary>
        public static LogManager Log { get; private set; }

        /// <summary>
        /// 计时器管理器
        /// </summary>
        public static TimerManager Timer { get; private set; }

        public static CacheManager Cache { get; private set; }

        public static void Awake()
        {
            Log = GetGameManager<LogManager>();
            Timer = GetGameManager<TimerManager>();
            Cache = GetGameManager<CacheManager>();
            Init();
        }

        private static void Init()
        {
            foreach (var manager in SGameManagers)
            {
                manager.OnInit();
            }
        }

        public static void Start()
        {
            foreach (var manager in SGameManagers)
            {
                manager.OnStart();
            }
            
        }

        public static void Update(float elapseSeconds, float realElapseSeconds)
        {
            foreach (var manager in SGameManagers)
            {
                manager.OnUpdate(elapseSeconds,realElapseSeconds);
            }
        }

        public static void ShutDown()
        {
            foreach (var manager in SGameManagers)
            {
                manager.ShutDown();
            }
            
            SGameManagers.Clear();
            
        }
    }
}

