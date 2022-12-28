
namespace SFramework.Core
{
    /// <summary>
    /// 任务接口
    /// </summary>
    public interface ITaskAgent<T> : IUpdate,IShutDown,IReference where T: TaskBase
    {
        /// <summary>
        /// 获取任务
        /// </summary>
        T Task
        {
            get;
        }

        /// <summary>
        /// 初始化任务
        /// </summary>
        void Initialize();
        
        /// <summary>
        /// 开始处理任务。
        /// </summary>
        /// <param name="task">要处理的任务。</param>
        /// <returns>开始处理任务的状态。</returns>
        StartTaskStatus Start(T task);

        /// <summary>
        /// 当完成任务的时候
        /// </summary>
        void OnComplete();

        /// <summary>
        /// 停止正在处理的任务并重置任务代理。
        /// </summary>
        void Reset();
    }
}