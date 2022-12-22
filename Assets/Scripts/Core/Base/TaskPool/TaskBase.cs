using UnityEngine;

namespace SFramework.Core
{
    public abstract class TaskBase : IReference
    {
        /// <summary>
        /// 任务默认优先级。
        /// </summary>
        public const int DefaultPriority = 0;
        
        private int m_SerialId;
        private string m_Tag;
        private int m_Priority;
        private object m_UserData;
        private bool m_Done;

        /// <summary>
        ///  获取任务的序列编号
        /// </summary>
        public int SerialId => m_SerialId;
        /// <summary>
        /// 获取任务的标签
        /// </summary>
        public string Tag => m_Tag;
        /// <summary>
        /// 获取任务的优先级
        /// </summary>
        public int Priority => m_Priority;
        /// <summary>
        /// 获取任务的用户自定义数据
        /// </summary>
        public object UserData => m_UserData;
        /// <summary>
        /// 获取任务是否完成
        /// </summary>
        public bool Done{
            get
            {
                return m_Done;
            }
            set
            {
                m_Done = value;
            }
        }
        
        /// <summary>
        /// 获取任务描述。
        /// </summary>
        public virtual string Description
        {
            get
            {
                return null;
            }
        }
        
        public TaskBase()
        {
            InitTaskDefault();
        }

        /// <summary>
        /// 对池进行初始化
        /// </summary>
        private void InitTaskDefault()
        {
            Initialize(0, null, 0, null);
        }

        /// <summary>
        /// 初始化任务基类。
        /// </summary>
        /// <param name="serialId">任务的序列编号。</param>
        /// <param name="tag">任务的标签。</param>
        /// <param name="priority">任务的优先级。</param>
        /// <param name="userData">任务的用户自定义数据。</param>
        internal void Initialize(int serialId, string tag, int priority, object userData)
        {
            m_SerialId = serialId;
            m_Tag = tag;
            m_Priority = priority;
            m_UserData = userData;
            m_Done = false;
        }
        /// <summary>
        /// 转换userdata
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUserData<T>() where T : class
        {
            if (UserData == null)
            {
                return default(T);
            }
            if (UserData.GetType() == typeof(T))
                return (T)UserData;
            return default(T);
        }

        public virtual void Clear()
        {
            InitTaskDefault();
        }

        public float LastUseTime { get; set; }
    }
}