using System;

namespace SFramework.Core
{
    public partial class ResourceManager_AB
    {
        public class BundleRequest : Request
        {

            /// <summary>
            /// bundle的映射
            /// </summary>
            public BundleRef bundleRef;

            /// <summary>
            /// bundle名称
            /// </summary>
            public string bundleName;
            /// <summary>
            /// 资源加载成功后的回调
            /// </summary>
            public LoadAssetCallBacks loadAssetCallBacks;
            /// <summary>
            /// 进行reset操作
            /// </summary>
            /// <param name="bundleName"></param>
            /// <param name="loadResourceMode"></param>
            public static BundleRequest Get(string bundleName, LoadResourceMode loadResourceMode, LoadAssetCallBacks loadAssetCallBacks)
            {
                BundleRequest bundleRequest = ReferencePool.Acquire<BundleRequest>();
                bundleRequest.bundleName = bundleName;
                bundleRequest.SetLoadResourceMode(loadResourceMode);
                bundleRequest.loadAssetCallBacks = loadAssetCallBacks;
                bundleRequest.Action();
                return bundleRequest;
            }
            
            /// <summary>
            /// 行为
            /// </summary>
            public override void Action()
            {
                ResourceLoader.ReloadAssetBundle(bundleName);
                ResourceLoader.AddBundleRequest(this);
            }

            public override void Clear()
            {
                base.Clear();
                bundleRef = null;
                loadAssetCallBacks = null;
            }

        
        }
   
    }
}
