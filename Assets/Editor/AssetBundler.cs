
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;



public class AssetBundler : MonoBehaviour {

    const string GUID_PREFIX = "guid:";

    [MenuItem("Asset Bundler/Build Asset Bundle")]
    static void BuildAssetBundle() {
        string bundleName = Application.productName.Replace(" ","");
        Debug.Log(bundleName);

        AssetBundleBuild[] buildMap = new AssetBundleBuild[1];

        buildMap[0].assetBundleName = bundleName;

        //TODO: Dont hardcode this
        string sceneName = "Assets/Scenes/GameScreen.unity";
        HashSet<string> sceneDependencies = new HashSet<string>();
        sceneDependencies.Add(sceneName);
        GetSceneDependencies(sceneName, sceneDependencies);

        buildMap[0].assetNames = new string[sceneDependencies.Count];
        sceneDependencies.CopyTo(buildMap[0].assetNames);

        if (!Directory.Exists("Assets/AssetBundles")) {
            Directory.CreateDirectory("Assets/AssetBundles");
        }

        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", buildMap, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

        Debug.Log("Success");
    }

    /// <summary>
    ///     We can't load the scene to collect dependencies, so let's get dirty and
    ///     parse the file for GUIDs.
    /// </summary>
    /// <param name="path">Path.</param>
    /// <param name="output">Output.</param>
    static void GetSceneDependencies(string path, HashSet<string> output) {
        using (StreamReader reader = new StreamReader(path)) {
            while (reader.Peek() >= 0) {
                string line = reader.ReadLine();
                if (!line.Contains(GUID_PREFIX))
                    continue;

                string guidPart = line.Split(',')[1];
                string guid = RemoveSubstring(guidPart, GUID_PREFIX).Trim();

                output.Add (AssetDatabase.GUIDToAssetPath(guid));
            }
        }
       
    }

    /// <summary>
    ///    Removes all instances of the given substring.
    /// </summary>
    /// <returns>The cleaned string.</returns>
    /// <param name="source">Source.</param>
    /// <param name="remove">Remove.</param>
    static string RemoveSubstring(string source, string remove) {
        int index = source.IndexOf(remove);
        string clean = (index < 0) ? source : source.Remove(index, remove.Length);

        if (clean.Length != source.Length)
            return RemoveSubstring(clean, remove);

        return clean;
    }

}
