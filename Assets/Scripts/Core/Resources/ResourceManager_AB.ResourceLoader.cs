using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFramework.Core
{
    public partial class ResourceManager_AB
    {
        public class ResourceLoader : IAwake
        {
            private SManifest m_Manifest;

            private Dictionary<string,BundleRef> m_CacheBundleRef = new Dictionary<string,BundleRef>();
            public void OnAwake()
            {
                //初始化Manifest
                m_Manifest.OnAwake();
                //初始化资源映射表 key => assetname  value => bundlename
            }

            /// <summary>
            /// 获取bundle信息
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public BundleInfo GetBundleInfo(string bundleName)
            { 
                return m_Manifest.GetBundleInfo(bundleName);
            }

            /// <summary>
            /// Bundle中是否含有资源
            /// </summary>
            /// <param name="bundleName"></param>
            /// <param name="assetName"></param>
            /// <returns></returns>
            public bool IsContainAsset(string bundleName, string assetName)
            { 
                return m_Manifest.IsContainAsset(bundleName, assetName);
            }

            /// <summary>
            /// 获取常驻bundle名称
            /// </summary>
            /// <returns></returns>
            public List<string> GetPermanentBundleNames()
            {
                return m_Manifest.PermanentBundles;
            }

            /// <summary>
            /// 检测是否为常驻bundle
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public bool IsPermanentBundle(string bundleName)
            {
                return m_Manifest.IsPermanentBundle(bundleName);
            }
            /// <summary>
            /// 获取bundle依赖
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public string[] GetDependencies(string bundleName)
            { 
                return m_Manifest.GetDependencies(bundleName);
            }

        }
    }
}
