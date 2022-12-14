using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace SFramework.Core
{

        /// <summary>
        /// Bundle类型
        /// </summary>
        public enum BundleType
        { 
            None = 0,
            /// <summary>
            /// 包含多个资源
            /// </summary>
            Contains = 1 << 1,
            /// <summary>
            /// 常驻
            /// </summary>
            Permanent = 1 << 2,

            /// <summary>
            /// 公用资源
            /// </summary>
            Public = 1 << 3,
            /// <summary>
            /// 允许立即释放
            /// </summary>
            AllowReleasse = 1 << 4,

        }
        /// <summary>
        /// Bundle 信息
        /// </summary>
        public class BundleInfo
        {
            /// <summary>
            /// Bundle名称
            /// </summary>
            public string bundleName;
            /// <summary>
            /// 包含的所有资源名称
            /// </summary>
            public string[] assets;
            /// <summary>
            /// Bundle类型
            /// </summary>
            public BundleType type;

            /// <summary>
            /// 大小
            /// </summary>
            public long size;
            /// <summary>
            /// 依赖信息
            /// </summary>
            public string[] dependencies;
            /// <summary>
            /// hash
            /// </summary>
            public string hash;
            /// <summary>
            /// 是否是场景
            /// </summary>
            public bool isScene;

            /// <summary>
            /// 文件的只读路径  streamingassetpath
            /// </summary>
            public string readOnlyPath;

            /// <summary>
            /// 文件的读写路径  persistentDataPath
            /// </summary>
            public string readWirtePath;

            /// <summary>
            /// 真实路径
            /// </summary>
            public string realPath;

          /// <summary>
          /// 真实路径是否为null
          /// </summary>
            public bool RealPathIsEmpty => string.IsNullOrEmpty(realPath);


            /// <summary>
            /// 检查bundle的路径是否存在
            /// </summary>
            /// <returns></returns>
            public bool CheckBundlePath()
            {
                return RealPathIsEmpty;
                if (File.Exists(readWirtePath))
                    realPath = readWirtePath;
                if (File.Exists(readOnlyPath) && RealPathIsEmpty)
                    realPath = readOnlyPath;
                return RealPathIsEmpty;

            }

            public bool CheckDependencies => dependencies!= null && dependencies.Length > 0;

            /// <summary>
            /// 是否为该类型
            /// </summary>
            /// <param name="bundleType"></param>
            /// <returns></returns>
            public bool IsType(BundleType bundleType)
            {          
                return (type & bundleType) == bundleType;
            }

            /// <summary>
            /// 是否包含资源
            /// </summary>
            /// <param name="assetName">资源名称</param>
            /// <returns></returns>
            public bool IsContainAsset(string assetName)
            {
                if (assets == null || assets.Length == 0)
                {
                    return false;
                }
                for (int i = 0; i < assets.Length; i++)
                {
                    if (assets[i].Equals(assetName))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// bundle Manifes文件
        /// </summary>
        public class SManifest : ScriptableObject , IAwake
        {
            public List<BundleInfo> bundles = new List<BundleInfo>();

            private Dictionary<string,BundleInfo> m_BundlesDic = new Dictionary<string,BundleInfo>();

            /// <summary>
            /// 每帧最大更新bundle个数
            /// </summary>
            public int maxLoadBundleCount = 10;
            /// <summary>
            /// Asset=>Bundle映射表
            /// </summary>
            private Dictionary<string,string> m_Asset2BundleDic = new Dictionary<string,string>();

            /// <summary>
            /// 切换场景时禁止卸载资源
            /// </summary>
            public List<string> ChangeSceneSkipBundles = new List<string>()
            { };

            /// <summary>
            /// 常驻资源
            /// </summary>
            public List<string> PermanentBundles = new List<string>()
            { };

            public void OnAwake()
            {
                m_BundlesDic.Clear();
                m_Asset2BundleDic.Clear();
                for (int i = 0; i < bundles.Count; i++)
                {
                    BundleInfo bundleInfo = bundles[i];
                    bundleInfo.readOnlyPath = Utility.Path.CombinePath(Utility.Path.ReadOnlyAssetBundlePath, bundleInfo.bundleName);
                    bundleInfo.readWirtePath = Utility.Path.CombinePath(Utility.Path.ReadWriteAssetBundlePath, bundleInfo.bundleName);
                    m_BundlesDic.Add(bundleInfo.bundleName, bundleInfo);
                    foreach (var item in bundleInfo.assets)
                    {
                        m_Asset2BundleDic.Add(item, bundleInfo.bundleName);
                    }
                }
                bundles.Clear();
                bundles = null;
            }
            /// <summary>
            /// 将asset名称转换为bundle名称
            /// </summary>
            /// <param name="assetName"></param>
            /// <returns></returns>
            public string AssetToBundleName(string assetName)
            {
                if (!m_Asset2BundleDic.ContainsKey(assetName))
                {
                    SGameEntry.Log.Error("Don't Find asset BundleName:{0}", assetName);
                    return null;
                }
                return m_Asset2BundleDic[assetName];
            }

            /// <summary>
            /// 增加切换场景不销毁资源
            /// </summary>
            /// <param name="bundleName"></param>
            public void AddChangeSceneSkipBundle(string bundleName)
            {
                if (ChangeSceneSkipBundles.Contains(bundleName)) return;
                ChangeSceneSkipBundles.Add(bundleName);
            }

            /// <summary>
            /// 是否跳过该资源的移除
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public bool IsSkipRemoveBundle(string bundleName)
            { 
                return IsChangeSceneSkipBundle(bundleName) || IsPermanentBundle(bundleName);
            }

            /// <summary>
            /// 是否为切换场景时跳过的资源
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public bool IsChangeSceneSkipBundle(string bundleName)
            {
                return ChangeSceneSkipBundles.Contains(bundleName);
            }

            /// <summary>
            /// 是否为常驻资源
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public bool IsPermanentBundle(string bundleName)
            { 
                return PermanentBundles.Contains(bundleName);
            }

            /// <summary>
            /// bundle中是否包含资源
            /// </summary>
            /// <param name="bundleName"></param>
            /// <param name="assetName"></param>
            /// <returns></returns>
            public bool IsContainAsset(string bundleName, string assetName)
            { 
                BundleInfo bundleInfo = GetBundleInfo(bundleName);
                return bundleInfo == null ? false:bundleInfo.IsContainAsset(assetName);
            }
            /// <summary>
            /// 获取bundle类型
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public BundleType GetBundleType(string bundleName)
            {
                BundleInfo bundleInfo = GetBundleInfo(bundleName);
                return bundleInfo == null ? BundleType.None : bundleInfo.type;
            }
            /// <summary>
            /// 获取bundle大小
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public long GetBundleSize(string bundleName)
            {
                BundleInfo bundleInfo = GetBundleInfo(bundleName);
                return bundleInfo == null ? 0 : bundleInfo.size;

            }

            /// <summary>
            /// 获取bundle依赖
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public string[] GetDependencies(string bundleName)
            {
                BundleInfo bundleInfo = GetBundleInfo(bundleName);
                return bundleInfo == null ? null : bundleInfo.dependencies;
            }
            /// <summary>
            /// 获取bundle信息
            /// </summary>
            /// <param name="bundleName"></param>
            /// <returns></returns>
            public BundleInfo GetBundleInfo(string bundleName)
            {
                if (string.IsNullOrEmpty(bundleName)) 
                {
                    SGameEntry.Log.Error("Manifest Get bundleName is NULL.");
                    return null;
                }
                BundleInfo bundleInfo = null;
                m_BundlesDic.TryGetValue(bundleName, out bundleInfo);
                return bundleInfo;
            }
        }
    }
