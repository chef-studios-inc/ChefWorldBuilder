using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Chef
{
    public class CreateAssetBundles
    {
        public static void BuildAllAssetBundles()
        {
            string assetBundleDirectory = "Assets/World/Build";
            if (!Directory.Exists(assetBundleDirectory))
            {
                Directory.CreateDirectory(assetBundleDirectory);
            }
            BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                            BuildAssetBundleOptions.None,
                                            BuildTarget.WebGL);
        }
    }
}
