﻿using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace SFramework.Core
{
    public partial class ResourceManager_AB
    {
        /// <summary>
        /// 资源加载器状态
        /// </summary>
        public enum ResourceLoaderStatus
        { 
            /// <summary>
            /// 等待中
            /// </summary>
            Wait,

            /// <summary>
            /// 执行中
            /// </summary>
            Processing,
        }

        public  class ResourceLoader : IAwake,IUpdate
        {
            private SManifest m_Manifest;

            /// <summary>
            /// 所有已加载的内容
            /// </summary>
            private  Dictionary<string,BundleRef> m_CacheBundleRef = new Dictionary<string,BundleRef>();


            private TaskPool<LoaderBundleTask, LoaderBundleAgent> m_BundleLoaderTask;

            public int BundleCount => m_CacheBundleRef.Count;


            public int MaxLoadBundleCount => m_Manifest.maxLoadBundleCount;
            public void OnAwake()
            {
                //初始化Manifest
                m_Manifest.OnAwake();
                m_BundleLoaderTask = new TaskPool<LoaderBundleTask, LoaderBundleAgent>(m_Manifest.maxLoadBundleCount);
            }

            public void OnUpdate(float elapseSeconds, float realElapseSeconds)
            {
                
            }



            /// <summary>
            /// 加载bundle
            /// </summary>
            /// <param name="bundleName"></param>
            public LoaderBundleTask AddLoaderTask(string bundleName ,LoadResourceMode loadResourceMode,LoadAssetCallBacks loadAssetCallBacks,int serialId = 0)
            {
                LoaderBundleTask loaderBundleTask = LoaderBundleTask.Get(bundleName, this, loadResourceMode, loadAssetCallBacks);
                loaderBundleTask.SerialId = serialId;
                m_BundleLoaderTask.AddTask(loaderBundleTask);
                return loaderBundleTask;
            }

            /// <summary>
            /// 尝试获取bundle
            /// </summary>
            /// <param name="bundleName"></param>
            /// <param name="bundleRef"></param>
            /// <returns></returns>
            public bool TryGetBundle(string bundleName , out BundleRef bundleRef)
            {
                return m_CacheBundleRef.TryGetValue(bundleName, out bundleRef);
            }

            /// <summary>
            /// 重新加载assetbundle
            /// </summary>
            public void ReloadAssetBundle(string bundleName)
            {
                if (!m_CacheBundleRef.TryGetValue(bundleName, out BundleRef bundleRef)) return;
                if (bundleRef == null) return;
                bundleRef.AssetBundle.Unload(false);
                m_CacheBundleRef.Remove(bundleName);
                ReferencePool.Release(bundleRef);
            }

            /// <summary>
            /// 获取bundle信息
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public  BundleInfo GetBundleInfo(string bundleName)
            { 
                return m_Manifest.GetBundleInfo(bundleName);
            }

            /// <summary>
            /// Bundle中是否含有资源
            /// </summary>
            /// <param name="bundleName"></param>
            /// <param name="assetName"></param>
            /// <returns></returns>
            public  bool IsContainAsset(string bundleName, string assetName)
            { 
                return m_Manifest.IsContainAsset(bundleName, assetName);
            }

            /// <summary>
            /// 获取常驻bundle名称
            /// </summary>
            /// <returns></returns>
            public  List<string> GetPermanentBundleNames()
            {
                return m_Manifest.PermanentBundles;
            }

            /// <summary>
            /// 检测是否为常驻bundle
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public  bool IsPermanentBundle(string bundleName)
            {
                return m_Manifest.IsPermanentBundle(bundleName);
            }
            /// <summary>
            /// 获取bundle依赖
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public  string[] GetDependencies(string bundleName)
            { 
                return m_Manifest.GetDependencies(bundleName);
            }

         
        }
    }
}
