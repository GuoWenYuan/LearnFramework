using System;

namespace SFramework.Core
{
    public interface IDataProvider<T>
    {
        /// <summary>
        /// 数据读取成功事件
        /// </summary>
        event EventHandler<ReadDataSuccessfulEventArgs> ReadDataSuccess;

        /// <summary>
        /// 数据读取中事件
        /// </summary>
        event EventHandler<ReadDataUpdateEventArgs> ReadDataUpdate;

        /// <summary>
        /// 数据读取失败事件
        /// </summary>
        event EventHandler<ReadDataFailureEventArgs> ReadDataFailure;

        /// <summary>
        /// 读取数据加载数据依赖事件
        /// </summary>
        event EventHandler<ReadDataDependencyAssetEventArgs> ReadDataDependency;

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="dataAssetName">读取数据名称</param>
        void ReadData(string dataAssetName);

        /// <summary>
        /// 读取数据
        /// </summary>
        /// <param name="dataAssetName">读取数据名称</param>
        /// <param name="priority">优先级</param>
        void ReadData(string dataAssetName, int priority);
        
        /// <summary>
        /// 读取数据。
        /// </summary>
        /// <param name="dataAssetName">内容资源名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        void ReadData(string dataAssetName, object userData);

        /// <summary>
        /// 读取数据。
        /// </summary>
        /// <param name="dataAssetName">内容资源名称。</param>
        /// <param name="priority">加载数据资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        void ReadData(string dataAssetName, int priority, object userData);

        /// <summary>
        /// 解析内容。
        /// </summary>
        /// <param name="dataString">要解析的内容字符串。</param>
        /// <returns>是否解析内容成功。</returns>
        bool ParseData(string dataString);
        
        
        /// <summary>
        /// 解析内容。
        /// </summary>
        /// <param name="dataString">要解析的内容字符串。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>是否解析内容成功。</returns>
        bool ParseData(string dataString, object userData);

        /// <summary>
        /// 解析内容。
        /// </summary>
        /// <param name="dataBytes">要解析的内容二进制流。</param>
        /// <returns>是否解析内容成功。</returns>
        bool ParseData(byte[] dataBytes);

        /// <summary>
        /// 解析内容。
        /// </summary>
        /// <param name="dataBytes">要解析的内容二进制流。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>是否解析内容成功。</returns>
        bool ParseData(byte[] dataBytes, object userData);

        /// <summary>
        /// 解析内容。
        /// </summary>
        /// <param name="dataBytes">要解析的内容二进制流。</param>
        /// <param name="startIndex">内容二进制流的起始位置。</param>
        /// <param name="length">内容二进制流的长度。</param>
        /// <returns>是否解析内容成功。</returns>
        bool ParseData(byte[] dataBytes, int startIndex, int length);

        /// <summary>
        /// 解析内容。
        /// </summary>
        /// <param name="dataBytes">要解析的内容二进制流。</param>
        /// <param name="startIndex">内容二进制流的起始位置。</param>
        /// <param name="length">内容二进制流的长度。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>是否解析内容成功。</returns>
        bool ParseData(byte[] dataBytes, int startIndex, int length, object userData);
    }
}