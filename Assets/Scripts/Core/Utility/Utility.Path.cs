using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework.Core
{
    public static partial class Utility
    {
        public static partial class Path
        {
            //�ȸ����·��
            public static string AppName => "Res";
            //�ƶ����ȸ�·��
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
            /// PC���ȸ�·���������ص�assetbundle���·��
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
            /// ��Դ�ȸ�·��
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

        }
    }

}
