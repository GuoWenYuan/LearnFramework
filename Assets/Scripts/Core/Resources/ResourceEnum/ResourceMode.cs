namespace SFramework.Core
{
    /// <summary>
    /// 资源模式
    /// </summary>
    public enum ResourceMode : byte
    {
        /// <summary>
        /// 在UnityEditor中
        /// </summary>
        Editor = 0,
        
        /// <summary>
        /// 预下载的可更新模式->Bundle模式
        /// </summary>
        Updatable,
        
        /// <summary>
        /// 使用时下载的可更新模式 -> 边下边玩模式
        /// </summary>
        UpdatableWhilePlaying,
        
        
    }
}