namespace SFramework.Core
{
    public class LoadAssetCallBacks
    {
        private readonly LoadAssetSuccessCallBack m_LoadAssetSuccessCallback;
        private readonly LoadAssetFailureCallBack m_LoadAssetFailureCallback;
        private readonly LoadAssetUpdateCallBack m_LoadAssetUpdateCallback;
        private readonly LoadAssetDependencyAssetCallBack m_LoadAssetDependencyAssetCallback;
        
               /// <summary>
        /// 初始化加载资源回调函数集的新实例。s
        /// </summary>
        /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
        public LoadAssetCallBacks(LoadAssetSuccessCallBack loadAssetSuccessCallback)
            : this(loadAssetSuccessCallback, null, null, null)
        {
        }

        /// <summary>
        /// 初始化加载资源回调函数集的新实例。
        /// </summary>
        /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
        /// <param name="loadAssetFailureCallback">加载资源失败回调函数。</param>
        public LoadAssetCallBacks(LoadAssetSuccessCallBack loadAssetSuccessCallback, LoadAssetFailureCallBack loadAssetFailureCallback)
            : this(loadAssetSuccessCallback, loadAssetFailureCallback, null, null)
        {
        }

        /// <summary>
        /// 初始化加载资源回调函数集的新实例。
        /// </summary>
        /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
        /// <param name="loadAssetUpdateCallback">加载资源更新回调函数。</param>
        public LoadAssetCallBacks(LoadAssetSuccessCallBack loadAssetSuccessCallback, LoadAssetUpdateCallBack loadAssetUpdateCallback)
            : this(loadAssetSuccessCallback, null, loadAssetUpdateCallback, null)
        {
        }

        /// <summary>
        /// 初始化加载资源回调函数集的新实例。
        /// </summary>
        /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
        /// <param name="loadAssetDependencyAssetCallback">加载资源时加载依赖资源回调函数。</param>
        public LoadAssetCallBacks(LoadAssetSuccessCallBack loadAssetSuccessCallback, LoadAssetDependencyAssetCallBack loadAssetDependencyAssetCallback)
            : this(loadAssetSuccessCallback, null, null, loadAssetDependencyAssetCallback)
        {
        }

        /// <summary>
        /// 初始化加载资源回调函数集的新实例。
        /// </summary>
        /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
        /// <param name="loadAssetFailureCallback">加载资源失败回调函数。</param>
        /// <param name="loadAssetUpdateCallback">加载资源更新回调函数。</param>
        public LoadAssetCallBacks(LoadAssetSuccessCallBack loadAssetSuccessCallback, LoadAssetFailureCallBack loadAssetFailureCallback, LoadAssetUpdateCallBack loadAssetUpdateCallback)
            : this(loadAssetSuccessCallback, loadAssetFailureCallback, loadAssetUpdateCallback, null)
        {
        }

        /// <summary>
        /// 初始化加载资源回调函数集的新实例。
        /// </summary>
        /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
        /// <param name="loadAssetFailureCallback">加载资源失败回调函数。</param>
        /// <param name="loadAssetDependencyAssetCallback">加载资源时加载依赖资源回调函数。</param>
        public LoadAssetCallBacks(LoadAssetSuccessCallBack loadAssetSuccessCallback, LoadAssetFailureCallBack loadAssetFailureCallback, LoadAssetDependencyAssetCallBack loadAssetDependencyAssetCallback)
            : this(loadAssetSuccessCallback, loadAssetFailureCallback, null, loadAssetDependencyAssetCallback)
        {
        }

        /// <summary>
        /// 初始化加载资源回调函数集的新实例。
        /// </summary>
        /// <param name="loadAssetSuccessCallback">加载资源成功回调函数。</param>
        /// <param name="loadAssetFailureCallback">加载资源失败回调函数。</param>
        /// <param name="loadAssetUpdateCallback">加载资源更新回调函数。</param>
        /// <param name="loadAssetDependencyAssetCallback">加载资源时加载依赖资源回调函数。</param>
        public LoadAssetCallBacks(LoadAssetSuccessCallBack loadAssetSuccessCallback, LoadAssetFailureCallBack loadAssetFailureCallback, LoadAssetUpdateCallBack loadAssetUpdateCallback, LoadAssetDependencyAssetCallBack loadAssetDependencyAssetCallback)
        {
            if (loadAssetSuccessCallback == null)
            {
                throw new SException("Load asset success callback is invalid.");
            }

            m_LoadAssetSuccessCallback = loadAssetSuccessCallback;
            m_LoadAssetFailureCallback = loadAssetFailureCallback;
            m_LoadAssetUpdateCallback = loadAssetUpdateCallback;
            m_LoadAssetDependencyAssetCallback = loadAssetDependencyAssetCallback;
        }

        /// <summary>
        /// 获取加载资源成功回调函数。
        /// </summary>
        public LoadAssetSuccessCallBack LoadAssetSuccessCallback
        {
            get
            {
                return m_LoadAssetSuccessCallback;
            }
        }

        /// <summary>
        /// 获取加载资源失败回调函数。
        /// </summary>
        public LoadAssetFailureCallBack LoadAssetFailureCallback
        {
            get
            {
                return m_LoadAssetFailureCallback;
            }
        }

        /// <summary>
        /// 获取加载资源更新回调函数。
        /// </summary>
        public LoadAssetUpdateCallBack LoadAssetUpdateCallback
        {
            get
            {
                return m_LoadAssetUpdateCallback;
            }
        }

        /// <summary>
        /// 获取加载资源时加载依赖资源回调函数。
        /// </summary>
        public LoadAssetDependencyAssetCallBack LoadAssetDependencyAssetCallback
        {
            get
            {
                return m_LoadAssetDependencyAssetCallback;
            }
        }
    }
}