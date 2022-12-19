using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SFramework.Core
{
    public static partial class ReferencePool
    {
        /// <summary>
        /// 所有的引用池
        /// </summary>
        private static readonly Dictionary<Type, ReferenceCollection> s_ReferenceCollections = new Dictionary<Type, ReferenceCollection>();
        
        /// <summary>
        /// 是否开启强制检查
        /// </summary>
        private static bool m_EnableStrictCheck = false;

        /// <summary>
        /// 卸载时间 ,默认15s
        /// </summary>
        private static float m_AutoReleaseInterval = 15;

        /// <summary>
        /// 卸载时间
        /// </summary>
        public static float AutoReleaseInterval => m_AutoReleaseInterval;


        /// <summary>
        /// 获取或设置是否开启强制检查。
        /// </summary>
        public static bool EnableStrictCheck
        {
            get
            {
                return m_EnableStrictCheck;
            }
          
        }

        /// <summary>
        /// 获取引用池的数量
        /// </summary>
        public static int Count => s_ReferenceCollections.Count;

        /// <summary>
        /// 当初始化
        /// </summary>
        public static void OnInit()
        {
            
            
        }

        /// <summary>
        /// 当开始，在初始化后执行
        /// </summary>
        /// <param name="enableStrictCheck">是否启用强制检查</param>
        /// <param name="unLoadAssetTime">自动卸载引用池时间</param>
        public static void OnStart(bool enableStrictCheck,float unLoadAssetTime)
        {
            SetEnableStrictCheck(enableStrictCheck);
            SetUnLoadAssetTime(unLoadAssetTime);
            SGameEntry.Timer.RegisterTimer(unLoadAssetTime, () =>
            {
                foreach (var reference in s_ReferenceCollections.Values)
                {
                    reference.UnLoadAssets();
                }
            },null,true,true,s_ReferenceCollections);
        }

        /// <summary>
        /// 写入是否开启强制检查
        /// </summary>
        public static void SetEnableStrictCheck(bool enableStrictCheck)
        {
            m_EnableStrictCheck = enableStrictCheck;
        }

        /// <summary>
        /// 设置卸载时间
        /// </summary>
        /// <param name="unLoadAssetTime"></param>
        public static void SetUnLoadAssetTime(float unLoadAssetTime)
        {
            m_AutoReleaseInterval = unLoadAssetTime;
        }

        /// <summary>
        /// 获取所有引用池的信息。
        /// </summary>
        /// <returns>所有引用池的信息。</returns>
        public static ReferencePoolInfo[] GetAllReferencePoolInfos()
        {
            int index = 0;
            ReferencePoolInfo[] results = null;

            lock (s_ReferenceCollections)
            {
                results = new ReferencePoolInfo[s_ReferenceCollections.Count];
                foreach (KeyValuePair<Type, ReferenceCollection> referenceCollection in s_ReferenceCollections)
                {
                    results[index++] = new ReferencePoolInfo(referenceCollection.Key, referenceCollection.Value.UnusedReferenceCount, referenceCollection.Value.UsingReferenceCount, referenceCollection.Value.AcquireReferenceCount, referenceCollection.Value.ReleaseReferenceCount, referenceCollection.Value.AddReferenceCount, referenceCollection.Value.RemoveReferenceCount);
                }
            }

            return results;
        }
        
        /// <summary>
        /// 清除所有引用池。
        /// </summary>
        public static void ClearAll()
        {
            lock (s_ReferenceCollections)
            {
                foreach (KeyValuePair<Type, ReferenceCollection> referenceCollection in s_ReferenceCollections)
                {
                    referenceCollection.Value.RemoveAll();
                }

                s_ReferenceCollections.Clear();
            }
        }


        /// <summary>
        /// update逻辑
        /// </summary>
        public static void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            foreach (var reference in s_ReferenceCollections.Values)
            {
                reference.OnUpdate(elapseSeconds,realElapseSeconds);
            }

            
 
        }

        /// <summary>
        /// 从池中取出对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Acquire<T>() where T : class, IReference, new()
        {
            return GetReferenceCollection(typeof(T)).Acquire<T>();
        }

        /// <summary>
        /// 从池中取出对象
        /// </summary>
        /// <param name="referenceType"></param>
        /// <returns></returns>
        public static IReference Acquire(Type referenceType)
        {
            return GetReferenceCollection(referenceType).Acquire();
        }
        
        /// <summary>
        /// 将引用归还引用池。
        /// </summary>
        /// <param name="reference">引用。</param>
        public static void Release(IReference reference)
        {
            if (reference == null)
            {
                throw new SException("Reference is invalid.");
            }

            Type referenceType = reference.GetType();
            InternalCheckReferenceType(referenceType);
            GetReferenceCollection(referenceType).Release(reference);
        }

        /// <summary>
        /// 从池中移除某对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static bool Remove<T>() where T : IReference
        {
            Type referenceType = typeof(T);
            return Remove(referenceType);
        }

        /// <summary>
        /// 从池中移除对象
        /// </summary>
        /// <param name="referenceType"></param>
        /// <returns></returns>
        public static bool Remove(Type referenceType)
        {
            if (s_ReferenceCollections.ContainsKey(referenceType))
            {
                s_ReferenceCollections[referenceType].RemoveAll();
                s_ReferenceCollections.Remove(referenceType);
                return true;
            }
            return false;
        }

        
        /// <summary>
        /// 向引用池中追加指定数量的引用。
        /// </summary>
        /// <typeparam name="T">引用类型。</typeparam>
        /// <param name="count">追加数量。</param>
        public static void Add<T>(int count) where T : class, IReference, new()
        {
            GetReferenceCollection(typeof(T)).Add<T>(count);
        }

        /// <summary>
        /// 向引用池中追加指定数量的引用。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <param name="count">追加数量。</param>
        public static void Add(Type referenceType, int count)
        {
            InternalCheckReferenceType(referenceType);
            GetReferenceCollection(referenceType).Add(count);
        }
        
        
        /// <summary>
        /// 从引用池中移除指定数量的引用。
        /// </summary>
        /// <typeparam name="T">引用类型。</typeparam>
        /// <param name="count">移除数量。</param>
        public static void Remove<T>(int count) where T : class, IReference
        {
            GetReferenceCollection(typeof(T)).Remove(count);
        }

        /// <summary>
        /// 从引用池中移除指定数量的引用。
        /// </summary>
        /// <param name="referenceType">引用类型。</param>
        /// <param name="count">移除数量。</param>
        public static void Remove(Type referenceType, int count)
        {
            InternalCheckReferenceType(referenceType);
            GetReferenceCollection(referenceType).Remove(count);
        }
        
        /// <summary>
        /// 根据类型获取引用池中对象
        /// </summary>
        /// <param name="referenceType"></param>
        /// <returns></returns>
        /// <exception cref="SException"></exception>
        private static ReferenceCollection GetReferenceCollection(Type referenceType)
        {
            if (referenceType == null)
            {
                throw new SException("ReferenceType is invalid.");
            }
            ReferenceCollection referenceCollection = null;
            lock (s_ReferenceCollections)
            {
                if (!s_ReferenceCollections.TryGetValue(referenceType,out referenceCollection))
                {
                    referenceCollection = new ReferenceCollection(referenceType);
                    s_ReferenceCollections.Add(referenceType,referenceCollection);
                }
            }

            return referenceCollection;

        }

        /// <summary>
        /// 若开启强制检查则进行类型检查
        /// </summary>
        /// <param name="referenceType"></param>
        private static void InternalCheckReferenceType(Type referenceType)
        {
            if (!m_EnableStrictCheck)
            {
                return;
            }

            if (referenceType == null)
            {
                throw new SException("ReferencePool referenceType is Null");
            }

            if (!referenceType.IsClass || referenceType.IsAbstract)
            {
                throw new SException(Utility.Text.Format("ReferencePool referenceType is not Class or is Abstract :{0}",referenceType.FullName));
            }

            if (!typeof(IReference).IsAssignableFrom(referenceType))
            {
                throw new SException(Utility.Text.Format("Reference type '{0}' is invalid.", referenceType.FullName));
            }
            
        }
    }

}
