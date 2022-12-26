namespace SFramework.Core
{
    public enum LoadResourceResult : byte
    {
        /// <summary>
        /// 无状态
        /// </summary>
        None = 0,


        /// <summary>
        /// 等待加载中
        /// </summary>
        Wait,
        /// <summary>
        /// 加载中
        /// </summary>
        Processing,
        /// <summary>
        /// 完成
        /// </summary>
        Complete,

        /// <summary>
        /// 取消加载
        /// </summary>
        Cancelled,

        /// <summary>
        /// 资源不存在。
        /// </summary>
        NotExist,

        /// <summary>
        /// 资源尚未准备完毕。
        /// </summary>
        NotReady,

        /// <summary>
        /// 依赖资源错误。
        /// </summary>
        DependencyError,

        /// <summary>
        /// 资源类型错误。
        /// </summary>
        TypeError,

        /// <summary>
        /// 加载资源错误。
        /// </summary>
        AssetError
    }
}