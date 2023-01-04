using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFramework.Core
{

        public class LoaderBundleTask : TaskBase
        {
            /// <summary>
            /// bundle的引用
            /// </summary>
            public BundleRef bundleRef;

            /// <summary>
            /// 资源加载器
            /// </summary>
            public ResourceLoader resourceLoader;

            /// <summary>
            /// 加载资源模式
            /// </summary>
            public LoadResourceMode loadResourceMode;

            /// <summary>
            /// 加载资源结果
            /// </summary>
            public LoadResourceResult loadResourceResult;

            /// <summary>
            /// 加载资源结束后的回调
            /// </summary>
            public LoadAssetCallBacks loadAssetCallBacks;

            /// <summary>
            /// 获取bundle加载的加载器
            /// </summary>
            /// <param name="bundleName"></param>
            /// <param name="resourceLoader"></param>
            /// <param name="loadResourceMode"></param>
            /// <param name="loadAssetCallBacks"></param>
            /// <returns></returns>
            public static LoaderBundleTask Get(string bundleName,ResourceLoader resourceLoader,LoadResourceMode loadResourceMode, LoadAssetCallBacks loadAssetCallBacks)
            {
                LoaderBundleTask task = ReferencePool.Acquire<LoaderBundleTask>();
                task.resourceLoader = resourceLoader;
                task.loadResourceMode = loadResourceMode;
                task.loadAssetCallBacks = loadAssetCallBacks;
                task.bundleRef = ReferencePool.Acquire<BundleRef>();
                task.bundleRef.name = bundleName;
                return task;
                
            }

            public override void Clear()
            {
                bundleRef = null;
                resourceLoader = null;
                loadResourceMode = LoadResourceMode.None;
                loadResourceResult = LoadResourceResult.None;
                base.Clear();
            }
        }

    }
  

