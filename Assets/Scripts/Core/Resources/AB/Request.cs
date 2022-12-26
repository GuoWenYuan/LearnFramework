using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFramework.Core
{
    public class Request : IReference
    {

        /// <summary>
        /// 加载中的状态
        /// </summary>
        public LoadResourceResult LoadResult{ get; protected set; } = LoadResourceResult.None;

        /// <summary>
        /// 资源加载的模式
        /// </summary>
        public LoadResourceMode LoadResourceMode { get; protected set; } = LoadResourceMode.None;

        /// <summary>
        /// 加载是否成功
        /// </summary>
        public bool isDone => LoadResult == LoadResourceResult.Complete;

        /// <summary>
        /// 加载进度
        /// </summary>
        public float progress { get; set; }

        /// <summary>
        /// 写入加载结果
        /// </summary>
        /// <param name="loadResourceResult"></param>
        public void SetLoadResult(LoadResourceResult loadResourceResult)
        {
            progress = 1;
            LoadResult = loadResourceResult;
        }

        /// <summary>
        /// 写入加载模式
        /// </summary>
        /// <param name="loadResourceMode"></param>
        public void SetLoadResourceMode(LoadResourceMode loadResourceMode)
        {
            LoadResourceMode = loadResourceMode;
        }


        /// <summary>
        /// 行为
        /// </summary>
        public virtual void Action()
        { 
        
        }
        #region IReference Members


        public float LastUseTime { get ; set; }

        public virtual void Clear()
        {
            LoadResult = LoadResourceResult.None;
            LoadResourceMode = LoadResourceMode.None;
        }

        

        #endregion
    }
}
