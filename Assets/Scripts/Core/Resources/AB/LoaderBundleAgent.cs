using System;
using System.Collections.Generic;
using UnityEngine;
namespace SFramework.Core
{

        /// <summary>
        /// 加载bundle的代理器
        /// </summary>
        public class LoaderBundleAgent : ITaskAgent<LoaderBundleTask>
        {
            #region Public Static Function
            /// <summary>
            /// 依赖加载器，通过Task的serialId来做任务标识 
            /// </summary>
            public static Dictionary<int, LoaderBundleDendencieTask> LoaderDependenciesTasks = new Dictionary<int, LoaderBundleDendencieTask>();

            /// <summary>
            /// 增加加载依赖
            /// </summary>
            /// <param name="loaderBundleTask"></param>
            public static void AddDependencies(LoaderBundleTask loaderBundleTask)
            {
                int id = loaderBundleTask.SerialId;
                if (!LoaderDependenciesTasks.ContainsKey(id))
                {
                    LoaderBundleDendencieTask dendencieTaskTag = ReferencePool.Acquire<LoaderBundleDendencieTask>();
                    dendencieTaskTag.SetId(id);
                    LoaderDependenciesTasks.Add(id, dendencieTaskTag);
  
                }
                LoaderDependenciesTasks[id].AddTaskTag(loaderBundleTask);

            }

            public static void RemoveBundleTask(LoaderBundleTask loaderBundleTask)
            {
                if (!LoaderDependenciesTasks.ContainsKey(loaderBundleTask.SerialId))
                {
                    LoaderDependenciesTasks[loaderBundleTask.SerialId].Remove(loaderBundleTask);
                }
            }

            /// <summary>
            /// 获取当前是否为主任务
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public static bool GetIsMainTask(int id)
            {
                return !LoaderDependenciesTasks.ContainsKey(id);
            }

            /// <summary>
            /// 移除依赖
            /// </summary>
            /// <param name="id"></param>
            public static void RemoveDependencies(int id)
            {
                if (LoaderDependenciesTasks.ContainsKey(id))
                {
                    ReferencePool.Release(LoaderDependenciesTasks[id]);
                    LoaderDependenciesTasks.Remove(id); 
                
                }
            }
            /// <summary>
            /// 获取所有依赖是否加载完成
            /// </summary>
            public static bool IsAllBundleDone(int id)
            {
                if (LoaderDependenciesTasks.ContainsKey(id))
                {
                    return LoaderDependenciesTasks[id].GetIsDone();
                }
                return true;

            }

            #endregion

            public LoaderBundleTask Task { get; set; }

            /// <summary>
            /// 是否是主bundle
            /// </summary>
            public bool isMainBundle = false;
            public void Initialize()
            {
               
            }

            public void OnUpdate(float elapseSeconds, float realElapseSeconds)
            {
                if (Task != null)
                {
                    if (Task.loadResourceMode == LoadResourceMode.Aysnc)
                    {
                        Task.Done = Task.bundleRef.bundleCreateRequest.isDone;
                    }
                    if (isMainBundle)
                    {
                        Task.Done = Task.Done && IsAllBundleDone(Task.SerialId);
                        if (Task.Done)
                        {
                            RemoveDependencies(Task.SerialId);
                        }
                    
                    }
                   
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
                //如果bundle已经加载了
                if (task.resourceLoader.TryGetBundle(task.bundleRef.name))
                {
                    HandlerBundleDependencies(bundleInfo);
                    return StartTaskStatus.Done;
                }
                //同步加载
                if (task.loadResourceMode == LoadResourceMode.UnAysnc)
                {
                    task.bundleRef.AssetBundle = AssetBundle.LoadFromFile(bundleInfo.realPath);
                    HandlerBundleDependencies(bundleInfo);
                    return StartTaskStatus.CanResume;
                }

                if (task.loadResourceMode == LoadResourceMode.Aysnc)
                {
                    task.bundleRef.bundleCreateRequest = AssetBundle.LoadFromFileAsync(bundleInfo.realPath);
                    HandlerBundleDependencies(bundleInfo);
                    return StartTaskStatus.CanResume;
                }
                return StartTaskStatus.Done;
            }

            public float LastUseTime { get; set; }

            /// <summary>
            /// 增加主资源加载依赖的方案
            /// </summary>
            /// <param name="bundleInfo"></param>
            public void HandlerBundleDependencies(BundleInfo bundleInfo)
            {
                isMainBundle = GetIsMainTask(Task.SerialId);
                if (bundleInfo.CheckDependencies)
                {
                    foreach (var item in bundleInfo.dependencies)
                    {
                        AddDependencies(Task.resourceLoader.AddLoaderTask(item, Task.loadResourceMode, null,Task.SerialId));
                    }
                }
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
                Task.resourceLoader.AddBundleRef(Task.bundleRef);
                RemoveBundleTask(Task);
                Task.loadAssetCallBacks.LoadAssetSuccessCallback?.Invoke(Task.bundleRef.name, null, 0, Task.bundleRef);
            }
        }
    }

