using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFramework.Core
{
    public partial class ResourceManager_AB
    {
       
        /// <summary>
        /// 加载bundle依赖的标记
        /// </summary>
        public struct BundleDependencieTaskTag
        {
            public BundleDependencieTaskTag(LoaderBundleTask task)
            {
                Task = task;
            }
            public LoaderBundleTask Task;

            public bool GetIsDone()
            {
                return Task == null ? true : Task.Done;
            }
        }


        /// <summary>
        /// 加载bundle依赖的任务 
        /// </summary>
        public class LoaderBundleDendencieTask : IReference
        {
            public int Id { get;private set; }
            private List<LoaderBundleTask> bundleDependencieTaskTags = new List<LoaderBundleTask>();

            public void SetId(int id)
            { 
                Id = id;
            }
            public void AddTaskTag(LoaderBundleTask bundleTask)
            {
                bundleDependencieTaskTags.Add(bundleTask);
            }

            public void Remove(LoaderBundleTask loaderBundleTask)
            {
                if (bundleDependencieTaskTags.Contains(loaderBundleTask))
                {
                    bundleDependencieTaskTags.Remove(loaderBundleTask);
                }
            }

            /// <summary>
            /// 获取是否所有加载任务都已经结束
            /// </summary>
            /// <returns></returns>
            public bool GetIsDone()
            {
                return bundleDependencieTaskTags.Count == 0;
            }


            public float LastUseTime { get ; set ; }

            public void Clear()
            {
                bundleDependencieTaskTags.Clear();
                Id = -1;
            }
        }
    }
   
}
