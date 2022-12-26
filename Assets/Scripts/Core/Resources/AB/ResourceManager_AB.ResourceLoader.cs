using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public  static class ResourceLoader
        {
            private static SManifest m_Manifest;

            private static Dictionary<string,BundleRef> m_CacheBundleRef = new Dictionary<string,BundleRef>();

            /// <summary>
            /// 所有的加载bundle的请求
            /// </summary>
            private static Queue<BundleRequest> S_BundleRequests = new Queue<BundleRequest>();

            /// <summary>
            /// 等待中的bundle加载请求
            /// </summary>
            private static Queue<BundleRequest> S_WaitBundleRequests = new Queue<BundleRequest>();

            /// <summary>
            /// 资源加载器状态
            /// </summary>
            public static ResourceLoaderStatus ResourceLoaderStatus { get; private set; } = ResourceLoaderStatus.Wait;


           

            public static int BundleCount => m_CacheBundleRef.Count;
            public static void OnAwake()
            {
                //初始化Manifest
                m_Manifest.OnAwake();
                //初始化资源映射表 key => assetname  value => bundlename
            }

            public static void OnUpdate()
            { 
                
            }
            /// <summary>
            /// 增加bundle请求
            /// </summary>
            /// <param name="bundleRequest"></param>
            public static void AddBundleRequest(BundleRequest bundleRequest)
            {
                S_WaitBundleRequests.Enqueue(bundleRequest);
            }
            
            /// <summary>
            /// 重新加载assetbundle
            /// </summary>
            public static void ReloadAssetBundle(string bundleName)
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
            public static BundleInfo GetBundleInfo(string bundleName)
            { 
                return m_Manifest.GetBundleInfo(bundleName);
            }

            /// <summary>
            /// Bundle中是否含有资源
            /// </summary>
            /// <param name="bundleName"></param>
            /// <param name="assetName"></param>
            /// <returns></returns>
            public static bool IsContainAsset(string bundleName, string assetName)
            { 
                return m_Manifest.IsContainAsset(bundleName, assetName);
            }

            /// <summary>
            /// 获取常驻bundle名称
            /// </summary>
            /// <returns></returns>
            public static List<string> GetPermanentBundleNames()
            {
                return m_Manifest.PermanentBundles;
            }

            /// <summary>
            /// 检测是否为常驻bundle
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public static bool IsPermanentBundle(string bundleName)
            {
                return m_Manifest.IsPermanentBundle(bundleName);
            }
            /// <summary>
            /// 获取bundle依赖
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public static string[] GetDependencies(string bundleName)
            { 
                return m_Manifest.GetDependencies(bundleName);
            }

        }
    }
}
