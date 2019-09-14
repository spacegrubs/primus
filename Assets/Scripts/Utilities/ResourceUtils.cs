using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public static class ResourceUtils
{
    public static string GetPrefabResourcePath(GameObject gameObject)
    {
        return AssetDatabase.GetAssetPath(gameObject).Replace("Assets/Resources/", "").Replace(".prefab", "");
    }
}
