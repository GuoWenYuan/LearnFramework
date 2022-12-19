using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework.Core
{
    public interface IAwake
    {
        public void OnAwake();
    }

    public interface IAwake<T>
    {
        public void OnAwake(T t);
    }

    public interface IAwake<T, T1>
    {
        public void OnAwake(T t , T1 t1);
    }

    public interface IAwake<T, T1, T2>
    {
        public void OnAwake(T t, T1 t1, T2 t2);
    }

}

