using System;

namespace SFramework.Core
{
    public sealed class DataProvider<T> : IDataProvider<T>
    {
        private const int BlockSize = 1024 * 4;
        private static byte[] s_CachedBytes = null;

        private readonly T m_Owner;
        
            
        public event EventHandler<ReadDataSuccessfulEventArgs> ReadDataSuccess;
        public event EventHandler<ReadDataUpdateEventArgs> ReadDataUpdate;
        public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure;
        public event EventHandler<ReadDataDependencyAssetEventArgs> ReadDataDependency;


        public DataProvider(T owner)
        {
            m_Owner = owner;
            ReadDataSuccess = null;
            ReadDataUpdate = null;
            ReadDataFailure = null;
            ReadDataDependency = null;
        }

        /// <summary>
        /// 获取缓冲二进制大小
        /// </summary>
        public static int CacheBytesSize => s_CachedBytes == null ? 0 : s_CachedBytes.Length;

        /// <summary>
        /// 确保二进制流缓存分配足够大小的内存并缓存
        /// </summary>
        /// <param name="ensureSize">要确保二进制流缓存分配内存的大小</param>
        /// <exception cref="SException"></exception>
        public static void EnsureCachedBytesSize(int ensureSize)
        {
            if (ensureSize < 0)
            {
                throw new SException("Ensure size is invalid.");
            }
            if (s_CachedBytes == null || s_CachedBytes.Length < ensureSize)
            {
                FreeCachedBytes();
                int size = (ensureSize - 1 + BlockSize) / BlockSize * BlockSize;
                s_CachedBytes = new byte[size];
            }
        }

        /// <summary>
        /// 释放缓存的二进制流。
        /// </summary>
        public static void FreeCachedBytes()
        {
            s_CachedBytes = null;
        }

        public void ReadData(string dataAssetName)
        {
            throw new NotImplementedException();
        }

        public void ReadData(string dataAssetName, int priority)
        {
            throw new NotImplementedException();
        }

        public void ReadData(string dataAssetName, object userData)
        {
            throw new NotImplementedException();
        }

        public void ReadData(string dataAssetName, int priority, object userData)
        {
            throw new NotImplementedException();
        }

        public bool ParseData(string dataString)
        {
            throw new NotImplementedException();
        }

        public bool ParseData(string dataString, object userData)
        {
            throw new NotImplementedException();
        }

        public bool ParseData(byte[] dataBytes)
        {
            throw new NotImplementedException();
        }

        public bool ParseData(byte[] dataBytes, object userData)
        {
            throw new NotImplementedException();
        }

        public bool ParseData(byte[] dataBytes, int startIndex, int length)
        {
            throw new NotImplementedException();
        }

        public bool ParseData(byte[] dataBytes, int startIndex, int length, object userData)
        {
            throw new NotImplementedException();
        }
    }
}