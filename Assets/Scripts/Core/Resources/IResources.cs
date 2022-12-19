using System;

namespace SFramework.Core
{
    public interface IResources
    {
        /// <summary>
        /// 只读资源路径
        /// </summary>
        string ReadOnlyPath
        {
            get;
        }

        /// <summary>
        /// 获取读写路径
        /// </summary>
        string ReadWritePath
        {
            get;
        }

        /// <summary>
        /// 资源模式
        /// </summary>
        ResourceMode ResourcesMode
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
        int AssetCount
        {
            get;
        }

        /// <summary>
        /// 资源数量
        /// </summary>
        int ResourcesCount
        {
            get;
        }

        /// <summary>
        /// 资源组 数量
        /// </summary>
        int ResourcesGroupCount
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
        /// 获取或设置资源对象池对象过期秒数。
        /// </summary>
        float AssetExpireTime
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置资源对象池的优先级。
        /// </summary>
        int AssetPriority
        {
            get;
            set;
        }
        /// <summary>
        /// 获取或设置资源对象池自动释放可释放对象的间隔秒数。
        /// </summary>
        float ResourceAutoReleaseInterval
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置资源对象池的容量。
        /// </summary>
        int ResourceCapacity
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置资源对象池对象过期秒数。
        /// </summary>
        float ResourceExpireTime
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置资源对象池的优先级。
        /// </summary>
        int ResourcePriority
        {
            get;
            set;
        }


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

    }
}