namespace SFramework.Core
{
    /// <summary>
    /// 加载资源成功事件
    /// </summary>
    /// <param name="assetName">资源名称</param>
    /// <param name="asset">已加载的资源</param>
    /// <param name="duration">加载的时间</param>
    /// <param name="userData">用户自定义数据</param>
    public delegate void LoadAssetSuccessCallBack(string assetName, object asset, float duration, object userData);

    /// <summary>
    /// 加载资源失败事件
    /// </summary>
    /// <param name="assetName">资源名称</param>
    /// <param name="loadResourceStatus">资源加载状态</param>
    /// <param name="errorMessage">错误信息</param>
    /// <param name="userData">用户自定义数据</param>
    public delegate void LoadAssetFailureCallBack(string assetName, LoadResourceStatus loadResourceStatus,
        string errorMessage, object userData);

    /// <summary>
    /// 资源加载中事件
    /// </summary>
    /// <param name="assetName">资源名称</param>
    /// <param name="progress">加载进度</param>
    /// <param name="userData">用户自定义数据</param>
    public delegate void LoadAssetUpdateCallBack(string assetName, float progress, object userData);

    /// <summary>
    /// 加载资源依赖事件
    /// </summary>
    /// <param name="assetName">资源名称</param>
    /// <param name="dependencyAssetName">依赖名称</param>
    /// <param name="loadedCount">当前已被加载的资源个数</param>
    /// <param name="totalCount">总体资源个数</param>
    /// <param name="userData">用户自定义数据</param>
    public delegate void LoadAssetDependencyAssetCallBack(string assetName, string dependencyAssetName, int loadedCount,
        int totalCount, object userData);
}
            
           