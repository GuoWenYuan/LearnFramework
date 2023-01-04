using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SFramework.Core
{
    public class AssetRequest<T> : IReference where T: UnityEngine.Object
    {
        public static AssetRequest<T> Get()
        {
            return ReferencePool.Acquire<AssetRequest<T>>();
        }


        public float LastUseTime { get; set; }

        public T asset;

        public event Action<T> completed;

        public void OnCompleted()
        {
            var saved = completed;
            completed?.Invoke(asset);
            completed -= saved;
        }

        public void Clear()
        {
            asset = null;
            completed = null;
            ReferencePool.Release(this);
        }
    }
}
