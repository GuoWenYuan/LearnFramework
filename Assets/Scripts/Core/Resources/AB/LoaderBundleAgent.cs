using System;
using System.Collections.Generic;
using UnityEngine;
namespace SFramework.Core
{
    public partial class ResourceManager_AB
    {

        /// <summary>
        /// 加载bundle的代理器
        /// </summary>
        public class LoaderBundleAgent : ITaskAgent<LoaderBundleTask>
        {
            /// <summary>
            /// 依赖加载器，通过Task的serialId来做任务标识 
            /// </summary>
            public static Dictionary<int, LoaderBundleTask> LoaderDependenciesTasks = new Dictionary<int, LoaderBundleTask>();


            public LoaderBundleTask Task { get; set; }

           
            public void Initialize()
            {
               
            }

            public void OnUpdate(float elapseSeconds, float realElapseSeconds)
            {
                if (Task != null && Task.bundleRef.bundleCreateRequest!=null)
                {
                    Task.Done = Task.bundleRef.bundleCreateRequest.isDone;
                }
            }

            public void Reset()
            {
                Task = null;
            }

            public void ShutDown()
            {
               
            }

            public StartTaskStatus Start(LoaderBundleTask task)
            {
                Task = task;
                //先确定bundle池中没有该对象
                if (task.resourceLoader.TryGetBundle(task.bundleRef.name, out BundleRef bundleRef))
                {
                    task.bundleRef = bundleRef;
                    return StartTaskStatus.Done;
                }
                //加载bundle时需要再次确认池中没有该对象
                BundleInfo bundleInfo = task.resourceLoader.GetBundleInfo(task.bundleRef.name);
                //检查bundle是否存在
                if (bundleInfo == null)
                {
                    task.loadAssetCallBacks.LoadAssetFailureCallback?.Invoke(task.bundleRef.name, LoadResourceResult.NotExistInfo,
                        Utility.Text.Format("不存在BundleInfo信息,BundleName:{0}",task.bundleRef.name),null);
                    return StartTaskStatus.UnknownError;
                   
                }
                //检查资源是否存在本地
                if (!bundleInfo.CheckBundlePath()) 
                {
                    task.loadAssetCallBacks.LoadAssetFailureCallback?.Invoke(task.bundleRef.name, 
                        LoadResourceResult.NotExist,Utility.Text.Format("本地没有Bundle文件,ReadOnlyPath:{0},ReadWirtePath:{1}",
                        bundleInfo.readOnlyPath,bundleInfo.readWirtePath), null);
                    return StartTaskStatus.UnknownError;
                }

                //同步加载
                if (task.loadResourceMode == LoadResourceMode.UnAysnc)
                {
                    task.bundleRef.AssetBundle = AssetBundle.LoadFromFile(bundleInfo.realPath);
                    AddMainLoaderBundleDependencies(bundleInfo);
                    return StartTaskStatus.CanResume;
                }

                if (task.loadResourceMode == LoadResourceMode.Aysnc)
                {
                    task.bundleRef.bundleCreateRequest = AssetBundle.LoadFromFileAsync(bundleInfo.realPath);
                    AddMainLoaderBundleDependencies(bundleInfo);
                    return StartTaskStatus.CanResume;
                }
                return StartTaskStatus.Done;
            }

            public float LastUseTime { get; set; }

            /// <summary>
            /// 增加主资源加载依赖的方案
            /// </summary>
            /// <param name="bundleInfo"></param>
            public void AddMainLoaderBundleDependencies(BundleInfo bundleInfo)
            {
                if (bundleInfo.CheckDependencies)
                {
                    foreach (var item in bundleInfo.dependencies)
                    {
                        AddLoaderDependenciesTask(Task.resourceLoader.AddLoaderTask(item, Task.loadResourceMode, null,Task.SerialId));
                    }
                }
            }

            public void AddLoaderDependenciesTask(LoaderBundleTask loaderBundleTask)
            {
                
            }

            public void Clear()
            {
                Task = null;
                if (LoaderDependenciesTasks != null)
                {
                    LoaderDependenciesTasks.Clear();
                    LoaderDependenciesTasks = null;
                }

            }

            public void OnComplete()
            {
                Task.loadAssetCallBacks.LoadAssetSuccessCallback?.Invoke(Task.bundleRef.name, null, 0, Task.bundleRef);
            }
        }
    }
}
