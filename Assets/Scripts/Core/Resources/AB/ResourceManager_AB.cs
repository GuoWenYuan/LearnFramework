using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SFramework.Core
{
    public partial class ResourceManager_AB : IResource
    {


        public string CurrentVariant => "";

        public ResourceMode ResourceMode => ResourceMode.Updatable;

        /// <summary>
        /// 资源加载器
        /// </summary>
        private ResourceLoader m_ResourceLoader;
        /// <summary>
        /// 从 bundle中加载的资源缓存池，在无引用的情况下会被定时清除
        /// </summary>
        private Dictionary<string, AssetRef> m_AssetRefDic = new Dictionary<string, AssetRef>();
        
        /// <summary>
        /// Asset和bundle名称的映射表
        /// </summary>
        private Dictionary<AssetRef, BundleRef> m_Asset2BundleNameDic = new Dictionary<AssetRef, BundleRef>();
        public int BundleCount => m_ResourceLoader.BundleCount;


        public float AssetAutoReleaseInterval { get; set; }
        public int AssetCapacity { get; set; }
        public float BundleAutoReleaseInterval { get ; set; }
        public int BundleCapacity { get; set; }

        public void OnAwake()
        {
            //初始化所有数据，后续数据将会由配置文件进行生成
            AssetAutoReleaseInterval = 10;
            BundleAutoReleaseInterval = 10;
            AssetCapacity = 10;
            BundleCapacity = 10;
            m_ResourceLoader = new ResourceLoader();
            m_ResourceLoader.OnAwake();
        }

        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            m_ResourceLoader.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="loadResourceMode"></param>
        /// <param name="loadAssetCallBacks"></param>
        /// <returns></returns>
        public void LoadAsset<T>(string assetName, LoadResourceMode loadResourceMode,ResourceType resourceType, LoadAssetCallBacks loadAssetCallBacks) where T : UnityEngine.Object
        {
            AssetRef assetRef = TryGetAsset(assetName);
            if (assetRef != null)
            {
                assetRef.Retain();
                loadAssetCallBacks.LoadAssetSuccessCallback?.Invoke(assetName, assetRef.Assets as T, 0, assetRef);
                return;
            }
            string bundleName = ConvertAsset2BundleName(assetName, resourceType);

            if (m_ResourceLoader.TryGetBundle(bundleName, out BundleRef bundleRef))
            {
                if (m_ResourceLoader.IsContainAsset(bundleName, assetName))
                {
                    assetRef = CreateAssetRef(assetName, bundleRef);

                    loadAssetCallBacks.LoadAssetSuccessCallback?.Invoke(assetName, assetRef.Assets, 0, assetRef);
                    return;
                }
                else
                {
                    loadAssetCallBacks.LoadAssetFailureCallback?.Invoke(assetName, LoadResourceResult.AssetError, "资源不存在", null);
                }
            }
            LoadBundle(bundleName, loadResourceMode, LoadBundleCallBacks<T>(loadAssetCallBacks, assetName));


        }

        /// <summary>
        /// bundle加载的回调
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="loadAssetCallBacks"></param>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public LoadAssetCallBacks LoadBundleCallBacks<T>(LoadAssetCallBacks loadAssetCallBacks,string assetName) where T : UnityEngine.Object
        {
            LoadAssetSuccessCallBack loadAssetSuccessCallBack = (bundleName, asset, duration, userData) =>
            {
                AssetRef assetRef = CreateAssetRef(assetName, userData as BundleRef);
                loadAssetCallBacks.LoadAssetSuccessCallback?.Invoke(bundleName, assetRef.Assets, duration, assetRef);
            };
            LoadAssetFailureCallBack loadAssetFailureCallBack = (bundleName, result, errorMessage, userData) =>
            { 
                loadAssetCallBacks?.LoadAssetFailureCallback?.Invoke(bundleName, result, errorMessage, userData);
            };
            return new LoadAssetCallBacks(loadAssetSuccessCallBack, loadAssetFailureCallBack, null, null);
        }
        /// <summary>
        /// 创建Asset对象
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="bundleRef"></param>
        /// <returns></returns>
        private AssetRef CreateAssetRef(string assetName, BundleRef bundleRef)
        {
            AssetRef assetRef = ReferencePool.Acquire<AssetRef>();
            m_AssetRefDic.Add(assetName, assetRef);
            assetRef.Retain();
            assetRef.Assets = bundleRef.AssetBundle.LoadAsset(assetName);
            m_Asset2BundleNameDic.Add(assetRef, bundleRef);
            return assetRef;
        }

        /// <summary>
        /// 加载Bundle
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bundleName"></param>
        /// <param name="loadResourceMode"></param>
        /// <param name="loadAssetCallBacks"></param>
        /// <returns></returns>
        public LoaderBundleTask LoadBundle(string bundleName, LoadResourceMode loadResourceMode ,LoadAssetCallBacks loadAssetCallBacks)
        {
            return m_ResourceLoader.LoadBundle(bundleName, loadResourceMode, loadAssetCallBacks);
        }



  

        /// <summary>
        /// 检测资源是否已经加载过了
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        private AssetRef TryGetAsset(string assetName)
        { 
            if (m_AssetRefDic.ContainsKey(assetName))
                return m_AssetRefDic[assetName];
            return null;
        }

        /// <summary>
        /// 将asset名称转换为bundle名称
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        private string ConvertAsset2BundleName(string assetName,ResourceType resourceType)
        {
            switch (resourceType)
            {
                case ResourceType.GameObject:
                    break;
                case ResourceType.Sprite:
                    //从图集信息缓存表中拿
                    break;
                case ResourceType.AnimationCilp:
                    //暂不需要
                    break;
                default:
                    break;
            }
            return assetName + Utility.Dafult.BundleSuffix;
        }

  

  

        public void ShutDown()
        {
           
        }
    }
}
