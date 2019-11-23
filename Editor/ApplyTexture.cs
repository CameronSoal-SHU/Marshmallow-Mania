using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ApplyTexture : EditorWindow
{
    GameObject go;
    Material mat;
    [MenuItem("Window/Utilities/Apply Texture")]
    private static void Window()
    {
        GetWindow(typeof(ApplyTexture));
    }
    public void OnInspectorUpdate()
    {
        Repaint();
    }
    private void OnGUI()
    {
        go = EditorGUILayout.ObjectField(go, typeof(GameObject), true) as GameObject;
        mat = EditorGUILayout.ObjectField(mat, typeof(Material), true) as Material;
        if (GUILayout.Button("Apply texture to all children"))
        {
            foreach (Renderer r in go.GetComponentsInChildren<Renderer>())
            {
                r.material = mat;
            }
        }
    }
}
