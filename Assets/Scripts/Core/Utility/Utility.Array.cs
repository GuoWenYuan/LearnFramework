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
            /// <summary>
            /// 是否含有某对象
            /// </summary>
            /// <param name="array"></param>
            /// <param name="match"></param>
            /// <typeparam name="T"></typeparam>
            /// <returns></returns>
            public static bool HasValue<T>(SLinkedList<T> array, FindHandler<T> match)
            {
                LinkedListNode<T> current = array.First;
                while (current != null)
                {
                    if (match(current.Value))
                    {
                        return true;
                    }

                    current = current.Next;    
                }

                return false;
            }
            
            
        }
        
    }
}

