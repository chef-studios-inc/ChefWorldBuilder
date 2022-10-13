using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chef
{
    public class Portal : MonoBehaviour
    {
        [System.Serializable]
        public enum Type { 
            External,
            World
        }

        public Type type;
        public string destination;
    }
}
