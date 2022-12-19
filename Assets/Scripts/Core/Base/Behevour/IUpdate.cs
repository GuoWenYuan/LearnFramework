using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SFramework.Core
{
    public interface IUpdate
    {
        public void OnUpdate(float elapseSeconds, float realElapseSeconds);
    }

}
