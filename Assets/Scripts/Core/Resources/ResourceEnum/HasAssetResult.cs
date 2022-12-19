namespace SFramework.Core
{
    public enum HasAssetResult : byte
    {
        /// <summary>
        /// 资源不存在
        /// </summary>
        NoExit = 0,
        
        /// <summary>
        /// 资源尚未准备完毕
        /// </summary>
        NotReady,
        
        /// <summary>
        /// 存在资源且存储在磁盘上
        /// </summary>
        AssetOnDisk,
        
        /// <summary>
        /// 存在资源且存储在文件系统里
        /// </summary>
        AssetOnFileSystem,
        
        /// <summary>
        /// 存在二进制资源且存储在磁盘上
        /// </summary>
        BinaryOnDisk,
        
        /// <summary>
        /// 存在二进制资源且存储在文件系统里
        /// </summary>
        BinaryOnFileSystem,
    }
}