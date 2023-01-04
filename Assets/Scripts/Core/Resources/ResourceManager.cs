using UnityEngine;

namespace SFramework.Core
{
    public partial class ResourceManager : SGameManager 
    {
        /// <summary>
        /// 资源处理器,分为bundle模式和资源模式
        /// </summary>
        private IResource m_ResourceManager;

        /// <summary>
        /// 资源模式 bundle/本地
        /// </summary>
        private ResourceMode m_ResourceType;

        

        public override void OnInit()
        {
            m_ResourceType = ResourceMode.Updatable;
            ///Bundle模式初始化
            m_ResourceManager = ResourceManagerHelper.GetResourceManager(m_ResourceType);
            m_ResourceManager.OnAwake();

        }
        /// <summary>
        /// 加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetName"></param>
        /// <param name="resourceType"></param>
        /// <param name="resourceMode"></param>
        /// <returns></returns>
        public AssetRequest<T> LoadAsset<T>(string assetName, ResourceType resourceType, LoadResourceMode resourceMode = LoadResourceMode.Aysnc) where T : UnityEngine.Object
        {
            AssetRequest<T> assetRequest = AssetRequest<T>.Get();
            LoadAssetSuccessCallBack loadAssetSuccessCallBack = (assetName, asset, duration, userdata) => 
            {
                assetRequest.asset = asset as T;
                assetRequest.OnCompleted();
            };

            LoadAssetFailureCallBack loadAssetFailureCallBack = (assetName, loadresult, errormessage, userdata) =>
            {
                assetRequest.asset = null;
                assetRequest.OnCompleted();
                SGameEntry.Log.Error("加载资源失败,加载结果：{0},错误信息:{1}", loadresult.ToString(), errormessage);
            };
            LoadAssetCallBacks loadAssetCallBacks = new LoadAssetCallBacks(loadAssetSuccessCallBack);
            m_ResourceManager.LoadAsset<T>(assetName, resourceMode, resourceType, loadAssetCallBacks);
            return assetRequest;
        }

        public AssetRequest<GameObject> CreateGameObject(string assetName)
        {
            AssetRequest<GameObject> assetRequest = LoadAsset<GameObject>(assetName, ResourceType.GameObject);
            AssetRequest<GameObject> gameObjectRequest = AssetRequest<GameObject>.Get();
            assetRequest.completed += gameobject =>
            {
                GameObject instance = Object.Instantiate<GameObject>(gameobject);
                gameObjectRequest.asset = instance;
                gameObjectRequest.OnCompleted();
            };

            return gameObjectRequest;
        }

   
        public void RemoveGameObject(GameObject go)
        { 
            
        }


        public override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            m_ResourceManager.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        public override void ShutDown()
        {
            m_ResourceManager.ShutDown();
        }
  


    }
}
