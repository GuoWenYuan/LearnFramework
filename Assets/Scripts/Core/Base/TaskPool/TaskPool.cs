using System.Collections;
using System.Collections.Generic;

namespace SFramework.Core
{
    public class TaskPool<T> : IUpdate , IShutDown where T: TaskBase
    {
        /// <summary>
        /// 闲置的任务
        /// </summary>
        private readonly Stack<ITaskAgent<T>> m_FreeAgents;
        /// <summary>
        /// 工作中的任务
        /// </summary>
        private readonly SLinkedList<ITaskAgent<T>> m_WorkingAgents;
        
        /// <summary>
        /// 等待中的任务
        /// </summary>
        private readonly SLinkedList<T> m_WaitingTasks;

        /// <summary>
        /// 任务是否在暂停中
        /// </summary>
        private bool m_Paused;

        public TaskPool()
        {
            m_FreeAgents = new Stack<ITaskAgent<T>>();
            m_WorkingAgents = new SLinkedList<ITaskAgent<T>>();
            m_WaitingTasks = new SLinkedList<T>();
        }
        /// <summary>
        /// 获取或设置任务池是否被暂停。
        /// </summary>
        public bool Paused
        {
            get
            {
                return m_Paused;
            }
            set
            {
                m_Paused = value;
            }

        }
        /// <summary>
        /// 获取任务代理总数量。
        /// </summary>
        public int TotalAgentCount => FreeAgentCount + WorkingAgentCount;

        /// <summary>
        /// 闲置任务代理个位
        /// </summary>
        public int FreeAgentCount => m_FreeAgents.Count;
        /// <summary>
        /// 工作中的代理个数
        /// </summary>
        public int WorkingAgentCount => m_WorkingAgents.Count;

        /// <summary>
        /// 等待中的代理个数
        /// </summary>
        public int WaitingTaskCount => m_WaitingTasks.Count;


        /// <summary>
        /// 任务池轮询
        /// </summary>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (m_Paused)
            {
                return;
            }
            
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void ShutDown()
        {
           
        }

        /// <summary>
        /// 增加任务代理
        /// </summary>
        /// <param name="agent"></param>
        public void AddAgent(ITaskAgent<T> agent)
        {
            if (agent == null)
            {
                throw new SException("Task agent is invalid.");
            }
            agent.Initialize();
            m_FreeAgents.Push(agent);
        }
        
        
    }
}