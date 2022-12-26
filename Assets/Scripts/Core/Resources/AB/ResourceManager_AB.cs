using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SFramework.Core
{
    public partial class ResourceManager_AB : SGameManager,IResource
    {
        public override int Priority => 3;


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
        /// 从资源池中实例化的gameobejct   key = instanceid  value = gameobject target
        /// </summary>
        private Dictionary<int, AssetRef> m_AssetRefCloneDic = new Dictionary<int, AssetRef>();
        public int AssetCount => m_AssetRefCloneDic.Count;

   

     

        /// <summary>
        /// Asset和bundle名称的映射表
        /// </summary>
        private Dictionary<AssetRef, BundleRef> m_Asset2BundleNameDic = new Dictionary<AssetRef, BundleRef>();
        public int BundleCount => m_ResourceLoader.BundleCount;


        public float AssetAutoReleaseInterval { get; set; }
        public int AssetCapacity { get; set; }
        public float BundleAutoReleaseInterval { get ; set; }
        public int BundleCapacity { get; set; }

        public override void OnStart()
        {
            //初始化所有数据，后续数据将会由配置文件进行生成
            AssetAutoReleaseInterval = 10;
            BundleAutoReleaseInterval = 10;
            AssetCapacity = 10;
            BundleCapacity = 10;
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="loadResourceMode"></param>
        /// <param name="loadAssetCallBacks"></param>
        /// <returns></returns>
        public void Load<T>(string assetName, LoadResourceMode loadResourceMode,ResourceType resourceType, LoadAssetCallBacks loadAssetCallBacks) where T : UnityEngine.Object
        {
            AssetRef assetRef = TryGetAsset(assetName);
            if (assetRef != null)
            {
                assetRef.Retain();
                loadAssetCallBacks.LoadAssetSuccessCallback?.Invoke(assetName, assetRef.Assets as T, 0, assetRef);
                return;
            }
            string bundleName = ConvertAsset2BundleName(assetName, resourceType);
            BundleRef bundleRef = TryGetBundle(bundleName);
            if (bundleRef != null)
            {
                if (m_ResourceLoader.IsContainAsset(bundleName, assetName))
                {
                    assetRef = ReferencePool.Acquire<AssetRef>();
                    m_AssetRefDic.Add(assetName, assetRef);
                    assetRef.Retain();
                    m_Asset2BundleNameDic.Add(assetRef, bundleRef);

                    loadAssetCallBacks.LoadAssetSuccessCallback?.Invoke(assetName, assetRef.Assets as T, 0, assetRef);
                    return;
                }
            }
            
        }


        public T LoadAsset<T>(string bundleName, LoadAssetCallBacks loadAssetCallBacks) where T : UnityEngine.Object
        {
           
        }

        public T LoadAssetAsync<T>(string bundleName, LoadAssetCallBacks loadAssetCallBacks) where T : UnityEngine.Object
        {
           
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

        /// <summary>
        /// 检测bundle是否已经加载过了
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        private BundleRef TryGetBundle(string bundleName)
        { 
            if (m_BundleRefDic.ContainsKey(bundleName))
                return m_BundleRefDic[bundleName];
            return null;
        }

        public void SetCurrentVariant(string currentVariant)
        {
           
        }

        public void SetReadOnlyPath(string readOnlyPath)
        {
           
        }

        public void SetReadWritePath(string readWritePath)
        {
            
        }

        public void SetResourceMode(ResourceMode resourceMode)
        {
            
        }
    }
}
