
using System;

namespace SFramework.Core
{
    public class SEventArgs : EventArgs ,IReference
    {
        public SEventArgs()
        {
            
        }
        public virtual void Clear()
        {
           
        }

        public float LastUseTime { get; set; }
    }
}