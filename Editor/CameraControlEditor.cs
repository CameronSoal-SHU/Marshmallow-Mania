using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CameraControl))]
public class CameraControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Small Shake"))
        {
            (target as CameraControl).AddShake(0.2f);
        }
        if (GUILayout.Button("Big Shake"))
        {
            (target as CameraControl).AddShake(1.0f);
        }
    }
}
