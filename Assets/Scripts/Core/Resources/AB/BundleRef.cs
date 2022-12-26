using System;
using UnityEngine;

namespace SFramework.Core
{
    public class AssetRef : SReference, IReference
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name;

        /// <summary>
        /// 资源对象
        /// </summary>
        public UnityEngine.Object Assets;

        /// <summary>
        /// 对应的bundle
        /// </summary>
        public BundleRef BundleRef;

        public float LastUseTime { get; set; }

        public void Clear()
        {
            Name = string.Empty;
            Assets = null;
            BundleRef = null;
            refStatus = RefStatus.None;
            refCount = 0;
        }
    }

    public class BundleRef : SReference,IReference
    {
        /// <summary>
        /// bundle名称
        /// </summary>
        public string name;

        /// <summary>
        /// bundle路径
        /// </summary>
        public string path;

        /// <summary>
        /// 资源大小
        /// </summary>
        public long size;

        /// <summary>
        /// 资源加载的状态
        /// </summary>
        public LoadResourceResult loadResourceStatus;

        /// <summary>
        /// 资源异步加载对象
        /// </summary>
        public AssetBundleCreateRequest bundleCreateRequest;


        /// <summary>
        /// bundle对象
        /// </summary>
        public AssetBundle AssetBundle;


        public float LastUseTime { get ; set ; }

        public void Clear()
        {
            name = string.Empty;
            path = string.Empty;
            size = 0;
            loadResourceStatus = LoadResourceResult.None;
            refStatus = RefStatus.None;
            AssetBundle = null;

        }
    }

    /// <summary>
    /// Bundle状态
    /// </summary>
    public enum RefStatus
    { 
        None,
        /// <summary>
        /// 使用中
        /// </summary>
        Use,
        /// <summary>
        /// 可以被卸载
        /// </summary>
        CanRelease,
    }

    public class SReference
    {

        /// <summary>
        ///资源引用使用中的状态
        /// </summary>
        public RefStatus refStatus;
        public virtual bool IsUnused()
        {
            return refCount <= 0;
        }

        public int refCount;

        public virtual void Retain()
        {
            refCount++;
            refStatus = RefStatus.Use;

        }

        public virtual void Release()
        { 
            refCount--;
            if (IsUnused())
            {
                refStatus = RefStatus.CanRelease;
            }
        }
    }
}
