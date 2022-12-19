namespace SFramework.Core
{
    /// <summary>
    /// 读取数据成功时间参数
    /// </summary>
    public sealed class ReadDataSuccessfulEventArgs : SEventArgs
    {
        /// <summary>
        /// 数据资源名称
        /// </summary>
        public string DataAssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// 读取数据持续时间
        /// </summary>
        public float Duration
        {
            get;
            private set;
        }

        /// <summary>
        /// 配置文件数据的类型
        /// </summary>
        public object UserData
        {
            get;
            private set;
        }

        /// <summary>
        /// 创建一个新的数据读取成功事件参数
        /// </summary>
        /// <param name="dataAssetName">数据名称</param>
        /// <param name="duration">持续时间</param>
        /// <param name="userData">使用的data</param>
        /// <returns></returns>
        public static ReadDataSuccessfulEventArgs Create(string dataAssetName, float duration, object userData)
        {
            ReadDataSuccessfulEventArgs dataSuccessfulEventArgs = ReferencePool.Acquire<ReadDataSuccessfulEventArgs>();
            dataSuccessfulEventArgs.DataAssetName = dataAssetName;
            dataSuccessfulEventArgs.Duration = duration;
            dataSuccessfulEventArgs.UserData = userData;
            return dataSuccessfulEventArgs;
        }

        public override void Clear()
        {
            DataAssetName = null;
            Duration = 0f;
            UserData = null;
        }
    }
}