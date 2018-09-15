using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ListObjectDependancies : EditorWindow {

    static GameObject obj = null;


    [MenuItem("EditorTools/Utility/Asset packaging")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        ListObjectDependancies window = (ListObjectDependancies)EditorWindow.GetWindow(typeof(ListObjectDependancies));
        window.titleContent = new GUIContent("Asset Packaging Utility");
        window.Show();
    }

    void OnGUI()
    {
        obj = EditorGUI.ObjectField(new Rect(3, 3, position.width - 6, 20), "Asset Object", obj, typeof(GameObject)) as GameObject;

        if (obj)
        {
            Object[] roots = new Object[] { obj };

            if (GUI.Button(new Rect(3, 25, position.width - 6, 20), "Check Dependencies"))
            {
                Selection.objects = EditorUtility.CollectDependencies(roots);
            }

            if (GUI.Button(new Rect(3, 48, position.width - 6, 20), "Create AssetPackage"))
            {
                AssetDatabase.ExportPackage(
                    AssetDatabase.GetAssetPath(obj), 
                    obj.name+".unitypackage", 
                    ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Interactive
                );
            }
        }
        else
            EditorGUI.LabelField(new Rect(3, 25, position.width - 6, 20), "Missing:", "Select an object first");
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }
}
