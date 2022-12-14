using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework.Core
{
    public static partial class Utility
    {
        public static partial class Path
        {
            //热更存放路径
            public static string AppName => "Res";
            //移动端热更路径
            private static string S_MobileHotFixPath;
            public static string MobileHotFixPath {
                get {
                    if (string.IsNullOrEmpty(S_MobileHotFixPath))
                        S_MobileHotFixPath = Application.persistentDataPath + "/" + AppName + "/";
                    return S_MobileHotFixPath;
                }
            }

            private static string S_StreamingAssetsPath;
            /// <summary>
            /// PC端热更路径，即本地的assetbundle存放路径
            /// </summary>
            public static string StreamingAssetsPath 
            {
                get
                {
                    if (string.IsNullOrEmpty(S_StreamingAssetsPath))
                        S_StreamingAssetsPath = Application.streamingAssetsPath + "/";
                    return S_StreamingAssetsPath;
                }
            }

            /// <summary>
            /// 资源热更路径
            /// </summary>
            public static string HotFixDataPath 
            {
                get 
                {
                    if (Application.isMobilePlatform)
                    {
                        return MobileHotFixPath;
                    }
                    if (Application.isEditor)
                    { 
                        return StreamingAssetsPath;
                    }
                    if (Application.platform == RuntimePlatform.OSXEditor)
                    { 
                        int i = Application.dataPath.LastIndexOf('/');
                        return Application.dataPath.Substring(0, i + 1) + AppName + "/";
                    }
                    return "c:/" + AppName + "/";
                }
            }

            /// <summary>
            /// AssetBundle路径
            /// </summary>
            public static string ReadWriteAssetBundlePath
            {
                get 
                {
                    return HotFixDataPath + "AssetBundles";
                }
            }
            /// <summary>
            /// Bundle的只读路径
            /// </summary>
            public static string ReadOnlyAssetBundlePath
            {
                get
                {
                    return StreamingAssetsPath + "AssetBundles";
                }
            }
            /// <summary>
            /// 合并路径
            /// </summary>
            /// <param name="path1"></param>
            /// <param name="path2"></param>
            /// <returns></returns>
            public static string CombinePath(string path1 ,string path2)
            {
                return System.IO.Path.Combine(path1, path2);
            }
  



        }
    }

}
