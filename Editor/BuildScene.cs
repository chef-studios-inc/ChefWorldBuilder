using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chef
{
    public class CreateAssetBundles
    {
        public static void BuildAllAssetBundles()
        {
            string assetBundleDirectory = "Assets/World/WorkDir";
            if(Directory.Exists(assetBundleDirectory))
            {
                Directory.Delete(assetBundleDirectory, true);
            }
            Directory.CreateDirectory(assetBundleDirectory);

            string buildDir = "Assets/World/Build";
            if(Directory.Exists(buildDir))
            {
                Directory.Delete(buildDir, true);
            }
            Directory.CreateDirectory(buildDir);

            if (!AssetDatabase.CopyAsset("Assets/World/World.unity", "Assets/World/WorkDir/World_Copy.unity"))
            {
                Debug.LogError("Error copying world scene");
            }
            AssetImporter.GetAtPath("Assets/World/WorkDir/World_Copy.unity").SetAssetBundleNameAndVariant("world_build", "");
            for(int i = 0; i < 10; i++)
            {
                var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
                scene.name = $"scene_{i}";
                EditorSceneManager.SaveScene(scene, $"Assets/World/WorkDir/{scene.name}.unity");
                AssetImporter.GetAtPath($"Assets/World/WorkDir/{scene.name}.unity").SetAssetBundleNameAndVariant("world_build", "");
            }
            var sourceScene = EditorSceneManager.OpenScene("Assets/World/WorkDir/World_Copy.unity", OpenSceneMode.Single);
            var buildScenes = new Scene[10];
            for(int i = 0; i < 10; i++)
            {
                var scene_name = $"scene_{i}";
                var s = EditorSceneManager.OpenScene($"Assets/World/WorkDir/{scene_name}.unity", OpenSceneMode.Additive);
                buildScenes[i] = s;
            }
            var rootGOs = sourceScene.GetRootGameObjects();
            int j = 0;
            foreach(var rgo in rootGOs)
            {
                var destScene = buildScenes[j];
                EditorSceneManager.MoveGameObjectToScene(rgo, destScene);
                j = (j + 1) % buildScenes.Length;
            }

            EditorSceneManager.MarkAllScenesDirty();
            EditorSceneManager.SaveOpenScenes();

            BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                            BuildAssetBundleOptions.None,
                                            BuildTarget.WebGL);

            FileUtil.MoveFileOrDirectory("Assets/World/WorkDir/world_build", "Assets/World/Build/world_build");
            Directory.Delete(assetBundleDirectory, true);

            EditorSceneManager.OpenScene($"Assets/World/World.unity", OpenSceneMode.Single);
        }
    }
}
