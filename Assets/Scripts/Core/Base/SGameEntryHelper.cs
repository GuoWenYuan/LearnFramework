using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework.Core
{
    public static partial class SGameEntry
    {

        /// <summary>
        /// 所有的游戏对象管理类
        /// </summary>
        public static readonly SLinkedList<SGameManager> SGameManagers = new SLinkedList<SGameManager>();



        /// <summary>
        /// 获取游戏对象管理类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetGameManager<T>() where T : SGameManager
        {
            return GetManager<T>() as T;
            
        }

        private static SGameManager GetManager<T>() where T: SGameManager
         {
             foreach (SGameManager module in SGameManagers)
             {
                 if (module.GetType() == typeof(T))
                 {
                     return module;
                 }
             }

             return CreateGameManager<T>();
         }

         private static SGameManager CreateGameManager<T>() where T : SGameManager
         {

             SGameManager manager = (SGameManager)Activator.CreateInstance(typeof(T));
             if (manager == null)
             {
                 new SException(Utility.Text.Format("Can't Create GameManager {0}", typeof(T).Name));
             }

             LinkedListNode<SGameManager> current = SGameManagers.First;
             while (current != null)
             {
                 if (manager.Priority > current.Value.Priority)
                 {
                     break;
                 }

                 current = current.Next;
             }

             if (current != null)
             {
                 SGameManagers.AddBefore(current, manager);
             }
             else
             {
                 SGameManagers.AddLast(manager);
             }

             return manager;
         }


    }
}

