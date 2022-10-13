using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Chef
{
    public class PublicFunctions : MonoBehaviour
    {

        [MenuItem("ChefStudios/Start Test Server")]
        private static void StartTestServer()
        {
            CreateAssetBundles.BuildAllAssetBundles();
            Debug.LogFormat("Successfully built world");
            AssetBundleServer.StartServer();
            Debug.LogFormat("Serving the world");
            Application.OpenURL("https://theuniverse.gg/w/test");
        }

        [MenuItem("ChefStudios/Stop Test Server")]
        private static void StopTestServer()
        {
            AssetBundleServer.StopServer();
        }

        [MenuItem("ChefStudios/Deploy Your World")]
        private static void DeployYourWorld()
        {
            CreateAssetBundles.BuildAllAssetBundles();
            var path = Path.Combine(Application.dataPath, "World/Build/world", "../");
            Application.OpenURL("file:///" + path);
            Application.OpenURL("https://theuniverse.gg/worlds");
        }
    }

}
