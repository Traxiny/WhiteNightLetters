using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
public class PrefabListManager : MonoBehaviour {
    
    [MenuItem("CustomMenu/CreatePrefabList")]
    public static void CreatePrefabListAsset()
    {
        PrefabList prefabList = ScriptableObject.CreateInstance<PrefabList>();
        prefabList.prefabs.AddRange(LoadPrefabs("Letters-Fixed/"));

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/AlphabetPrefabs.asset");

        AssetDatabase.CreateAsset(prefabList, assetPathAndName);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = prefabList;
    }

    static GameObject[] LoadPrefabs(string folderPath)
    {
        Object[] loadedPrefabs = Resources.LoadAll(folderPath, typeof(GameObject));
        GameObject[] prefabs = new GameObject[loadedPrefabs.Length];
        
        for (int i = 0; i < loadedPrefabs.Length; i++)
        {
            prefabs[i] = loadedPrefabs[i] as GameObject;
        }

        return prefabs;
    }
}
