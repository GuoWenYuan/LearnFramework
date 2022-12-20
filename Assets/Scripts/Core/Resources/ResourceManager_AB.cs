using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFramework.Core
{
    public class ResourceManager_AB : SGameManager,IResources
    {
        public override int Priority => 3;

        public string ReadOnlyPath => Utility.Path.StreamingAssetsPath;

        public string ReadWritePath => Utility.Path.HotFixDataPath;

        public ResourceMode ResourcesMode => ResourceMode.Updatable;

        public string CurrentVariant => "";

        public int AssetCount => throw new NotImplementedException();

        public int ResourcesCount => throw new NotImplementedException();

        public int ResourcesGroupCount => throw new NotImplementedException();

        public float AssetAutoReleaseInterval { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int AssetCapacity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float AssetExpireTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int AssetPriority { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float ResourceAutoReleaseInterval { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ResourceCapacity { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float ResourceExpireTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ResourcePriority { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
        {
            throw new NotImplementedException();
        }

        public IEnumerator LoadAssetAsync<T>(string assetName) where T : UnityEngine.Object
        {
            throw new NotImplementedException();
        }

        public void SetCurrentVariant(string currentVariant)
        {
           
        }

        public void SetReadOnlyPath(string readOnlyPath)
        {
           
        }

        public void SetReadWritePath(string readWritePath)
        {
            
        }

        public void SetResourceMode(ResourceMode resourceMode)
        {
            
        }
    }
}
