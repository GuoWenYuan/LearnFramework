using System;

namespace SFramework.Core
{
    public class DataProvider<T> : IDataProvider<T>
    {
        private const int BlockSize = 1024 * 4;
        private static byte[] s_CachedBytes = null;

        private readonly T m_Owner;
        
            
        public event EventHandler<ReadDataSuccessfulEventArgs> ReadDataSuccess;
        public event EventHandler<ReadDataUpdateEventArgs> ReadDataUpdate;
        public event EventHandler<ReadDataFailureEventArgs> ReadDataFailure;
        public event EventHandler<ReadDataDependencyAssetEventArgs> ReadDataDependency;
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