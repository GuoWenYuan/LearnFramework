using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFramework.Core
{
    public partial class ResourceManager_AB : SGameManager,IResource
    {
        public override int Priority => 3;


        public string CurrentVariant => "";

        public ResourceMode ResourceMode => ResourceMode.Updatable;


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
        /// bundle资源池
        /// </summary>
        private Dictionary<string, BundleRef> m_BundleRefDic = new Dictionary<string, BundleRef>();
        public int BundleCount => m_BundleRefDic.Count;


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





        public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
        {
           
        }

        public IEnumerator LoadAssetAsync<T>(string assetName) where T : UnityEngine.Object
        {
           
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
