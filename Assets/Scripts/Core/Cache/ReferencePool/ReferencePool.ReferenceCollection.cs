using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace SFramework.Core
{
    public static partial class ReferencePool 
    {
        public sealed class ReferenceCollection
        {
            /// <summary>
            /// 引用池，clear的时候会自动调用clear方法
            /// </summary>
            private readonly SLinkedList<IReference> m_References;
            /// <summary>
            /// 当前引用池类型
            /// </summary>
            private readonly Type m_ReferenceType;
            /// <summary>
            /// 使用引用池个数
            /// </summary>
            private int m_UsingReferenceCount;
            /// <summary>
            /// 当前取用个数
            /// </summary>
            private int m_AcquireReferenceCount;
            /// <summary>
            /// 当前释放个数
            /// </summary>
            private int m_ReleaseReferenceCount;
            /// <summary>
            /// 增加引用个数
            /// </summary>
            private int m_AddReferenceCount;
            /// <summary>
            /// 移除引用个数
            /// </summary>
            private int m_RemoveReferenceCount;


            
            /// <summary>
            /// 初始化
            /// </summary>
            /// <param name="referenceType">当前引用池的类型</param>
            public ReferenceCollection(Type referenceType)
            {
                m_References = new SLinkedList<IReference>();
                m_ReferenceType = referenceType;
                m_UsingReferenceCount = 0;
                m_AcquireReferenceCount = 0;
                m_ReleaseReferenceCount = 0;
                m_AddReferenceCount = 0;
                m_RemoveReferenceCount = 0;
            }

            /// <summary>
            /// 当前引用池类型
            /// </summary>
            public Type ReferenceType => m_ReferenceType;

            /// <summary>
            /// 当前引用池中未使用的对象
            /// </summary>
            public int UnusedReferenceCount => m_References.Count;


            /// <summary>
            /// 当前引用池在使用的对象个数
            /// </summary>
            public int UsingReferenceCount => m_UsingReferenceCount;


            /// <summary>
            /// 从引用池中取出的对象
            /// </summary>
            public int AcquireReferenceCount => m_AcquireReferenceCount;


            /// <summary>
            /// 当前引用池释放个数
            /// </summary>
            public int ReleaseReferenceCount => m_ReleaseReferenceCount;

            /// <summary>
            /// 当前增加引用池个数
            /// </summary>
            public int AddReferenceCount => m_AddReferenceCount;


            /// <summary>
            /// 当前移除引用池个数
            /// </summary>
            public int RemoveReferenceCount => m_RemoveReferenceCount;
            
            /// <summary>
            /// 获取引用池对象
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public T Acquire<T>() where T : class, IReference, new()
            {
                if (typeof(T) != m_ReferenceType)
                {
                    new SException("Type is invalid.");
                }

                m_UsingReferenceCount++;
                m_AcquireReferenceCount++;
                lock (m_References)
                {
                    if (m_References.Count > 0)
                    {
                        T value = (T)m_References.First.Value;
                        m_References.RemoveFirst();
                        return value;
                    }
                }

                m_AddReferenceCount++;
                return new T();

            }

            /// <summary>
            /// 获取引用池对象
            /// </summary>
            /// <returns></returns>
            public IReference Acquire()
            {
                m_UsingReferenceCount++;
                m_AcquireReferenceCount++;
                lock (m_References)
                {
                    if (m_References.Count > 0)
                    {
                        IReference value = (IReference)m_References.First.Value;
                        m_References.RemoveFirst();
                        return value;
                    }
                }

                m_AddReferenceCount++;
                return (IReference)Activator.CreateInstance(m_ReferenceType);
            }


            /// <summary>
            /// 释放
            /// </summary>
            /// <param name="reference"></param>
            /// <exception cref="SException"></exception>
            public void Release(IReference reference)
            {
                reference.Clear();
                lock (m_References)
                {
                    if (EnableStrictCheck && m_References.Contains(reference))
                    {
                        throw new SException("he reference has been released.");
                    }
                    m_References.AddLast(reference);
                    reference.LastUseTime = SGameEntry.Timer.UnityTime.RealtimeSinceStartup;
                }
                m_ReleaseReferenceCount++;
                m_UsingReferenceCount--;
            }

            /// <summary>
            /// 在池中增加个数
            /// </summary>
            /// <param name="count"></param>
            /// <typeparam name="T"></typeparam>
            /// <exception cref="SException"></exception>
            public void Add<T>(int count) where T: class, IReference, new()
            {
                if (typeof(T) != m_ReferenceType)
                {
                    throw new SException("Type is invalid.");
                }

                lock (m_References)
                {
                    m_AddReferenceCount += count;
                    while (count-- > 0)
                    {
                        m_References.AddLast(new T());
                    }
                }
            }
            
            /// <summary>
            /// 从池中增加个数
            /// </summary>
            /// <param name="count"></param>
            public void Add(int count)
            {
                lock (m_References)
                {
                    m_AddReferenceCount += count;
                    while (count-- > 0)
                    {
                        m_References.AddLast((IReference)Activator.CreateInstance(m_ReferenceType));
                    }
                }
            }

            /// <summary>
            /// 从池中移除个数
            /// </summary>
            /// <param name="count">长度</param>
            /// <typeparam name="T">类型</typeparam>
            public void Remove<T>(int count) where T : class, IReference, new()
            {
                if (typeof(T) != m_ReferenceType)
                {
                    throw new SException("Type is invalid.");
                }

                Remove(count);
            }

            /// <summary>
            /// 移除
            /// </summary>
            /// <param name="count"></param>
            public void Remove(int count)
            {
                lock (m_References)
                {
                    if (count > m_References.Count)
                    {
                        count = m_References.Count;
                    }
                    m_RemoveReferenceCount += count;
                    while (count-- > 0)
                    {
                       m_References.RemoveFirst();
                    }
                }
                
            }

            public void OnUpdate(float elapseSeconds, float realElapseSeconds)
            {
                
            }

            /// <summary>
            /// 卸载长时间内未被使用的对象
            /// </summary>
            public void UnLoadAssets()
            {
                lock (m_References)
                {
                    LinkedListNode<IReference> current = m_References.First;
                    while (current != null)
                    {
                        if (GetUnUseTime(current.Value) > ReferencePool.m_AutoReleaseInterval)
                        {
                            m_References.RemoveFirst();
                            m_RemoveReferenceCount++;
                            current = m_References.First;
                        }
                        current = current.Next;
                    }
                }
            }

            /// <summary>
            /// 获取未使用的时间
            /// </summary>
            /// <param name="reference"></param>
            /// <returns></returns>
            private float GetUnUseTime(IReference reference)
            {
                return SGameEntry.Timer.UnityTime.RealtimeSinceStartup - reference.LastUseTime;
            }


            /// <summary>
            /// 移除所有
            /// </summary>
            public void RemoveAll()
            {
                lock (m_References)
                {
                    m_RemoveReferenceCount += m_References.Count;
                    m_References.Clear();
                }
                
            }
        }
    }
}

