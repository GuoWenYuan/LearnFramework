using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework.Core
{
    public static partial class Utility
    {
        public static partial class Array
        {
            public delegate TKey SelectHandler<T, TKey>(T t);

            public delegate bool FindHandler<T>(T t);

            /// <summary>
            /// 升序
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <typeparam name="TKey"></typeparam>
            /// <param name="array"></param>
            /// <param name="handler"></param>
            public static void OrderByAscending<T, TKey>(IList<T> array, SelectHandler<T, TKey> handler)
                where TKey : IComparable<TKey>
            {
                for (var i = 0; i < array.Count - 1; i++)
                for (var j = i + 1; j < array.Count; j++)
                    if (handler(array[i]).CompareTo(handler(array[j])) > 0)
                    {
                        var temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
            }

            

            /// <summary>
            /// 降序
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <typeparam name="TKey"></typeparam>
            /// <param name="array"></param>
            /// <param name="handler"></param>
            public static void OrderByDescending<T, TKey>(T[] array, SelectHandler<T, TKey> handler)
                where TKey : IComparable<TKey>
            {
                for (var i = 0; i < array.Length - 1; i++)
                for (var j = i + 1; j < array.Length; j++)
                    if (handler(array[i]).CompareTo(handler(array[j])) < 0)
                    {
                        var temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
            }

            /// <summary>
            /// 按条件查找最大
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <typeparam name="TKey"></typeparam>
            /// <param name="array"></param>
            /// <param name="handler"></param>
            /// <returns></returns>
            public static T Max<T, TKey>(T[] array, SelectHandler<T, TKey> handler) where TKey : IComparable<TKey>
            {
                var t = array[0];
                for (var i = 1; i < array.Length; i++)
                    if (handler(t).CompareTo(handler(array[i])) < 0)
                        t = array[i];
                return t;
            }

            /// <summary>
            /// 按条件查找最小
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <typeparam name="TKey"></typeparam>
            /// <param name="array"></param>
            /// <param name="handler"></param>
            /// <returns></returns>
            public static T Min<T, TKey>(T[] array, SelectHandler<T, TKey> handler) where TKey : IComparable<TKey>
            {
                var t = array[0];
                for (var i = 1; i < array.Length; i++)
                    if (handler(t).CompareTo(handler(array[i])) > 0)
                        t = array[i];
                return t;
            }

            /// <summary>
            /// 查找单个
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="array"></param>
            /// <param name="handler"></param>
            /// <returns></returns>
            public static T Find<T>(T[] array, FindHandler<T> handler)
            {
                for (var i = 0; i < array.Length; i++)
                    if (handler(array[i]))
                        return array[i];
                return default(T);
            }

            /// <summary>
            /// 查找所有
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="array"></param>
            /// <param name="handler"></param>
            /// <returns></returns>
            public static T[] FindAll<T>(T[] array, FindHandler<T> handler)
            {
                var list = new List<T>();
                for (var i = 0; i < array.Length; i++)
                    if (handler(array[i]))
                        list.Add(array[i]);
                return list.ToArray();
            }

            /// <summary>
            /// 按照条件转换
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <typeparam name="TKey"></typeparam>
            /// <param name="array"></param>
            /// <param name="handler"></param>
            /// <returns></returns>
            public static TKey[] Select<T, TKey>(T[] array, SelectHandler<T, TKey> handler)
            {
                var keys = new TKey[array.Length];
                for (var i = 0; i < array.Length; i++) keys[i] = handler(array[i]);
                return keys;
            }
        }
    }
}