using System;
using System.Collections;

namespace SFramework.Core
{
    public interface IResource : IAwake ,IUpdate, IShutDown
    {

        /// <summary>
        /// 资源模式
        /// </summary>
        ResourceMode ResourceMode
        {
            get;
        }

        /// <summary>
        /// 获取当前变体
        /// </summary>
        string CurrentVariant
        {
            get;
        }



        /// <summary>
        /// 资源数量
        /// </summary>
        int BundleCount
        {
            get;
        }


        /// <summary>
        /// 获取或设置资源对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        float AssetAutoReleaseInterval
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置资源对象池的容量。
        /// </summary>
        int AssetCapacity
        {
            get;
            set;
        }




        /// <summary>
        /// 获取或设置资源对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        float BundleAutoReleaseInterval
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置资源对象池的容量。
        /// </summary>
        int BundleCapacity
        {
            get;
            set;
        }

  



        /*
        /// <summary>
        /// 设置资源只读区路径。
        /// </summary>
        /// <param name="readOnlyPath">资源只读区路径。</param>
        void SetReadOnlyPath(string readOnlyPath);

        /// <summary>
        /// 设置资源读写区路径。
        /// </summary>
        /// <param name="readWritePath">资源读写区路径。</param>
        void SetReadWritePath(string readWritePath);

        /// <summary>
        /// 设置资源模式。
        /// </summary>
        /// <param name="resourceMode">资源模式。</param>
        void SetResourceMode(ResourceMode resourceMode);

        /// <summary>
        /// 设置当前变体。
        /// </summary>
        /// <param name="currentVariant">当前变体。</param>
        void SetCurrentVariant(string currentVariant);
        */
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <returns></returns>
        void LoadAsset<T>(string assetName, LoadResourceMode loadResourceMode, ResourceType resourceType, LoadAssetCallBacks loadAssetCallBacks) where T : UnityEngine.Object;

        
    }
}