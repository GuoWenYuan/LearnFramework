using UnityEngine;

namespace SFramework.Core
{
    public partial class TimerManager
    {
        public class SUnityTime
        {
            public float RealtimeSinceStartup => Time.realtimeSinceStartup;
            public float DeltaTime => Time.deltaTime;
        }
    }

}