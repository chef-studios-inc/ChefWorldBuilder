using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chef
{
    public class Environment : MonoBehaviour
    {
        [System.Serializable]
        public struct FogSettings
        {
            public bool fogEnabled;
            public float fogStartDistance;
            public float fogEndDistance;

            public Color fogColor;
            public FogMode fogMode;
        }

        public Material skyboxMaterial;
        public FogSettings fogSettings = new FogSettings {
            fogEnabled = false,
            fogStartDistance = 20f,
            fogEndDistance = 100f,
            fogColor = Color.blue,
            fogMode = FogMode.Linear,
        };
        public float nearClippingDistance = 0.3f;
        public float farClippingDistance = 100f;
    }
}
