using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (MapGenerator))]
public class MapGeneratorEditor : Editor
{
    private bool autoUpdate = false;

    public override void OnInspectorGUI()
    {
        MapGenerator mapGenerator = target as MapGenerator;

        if (DrawDefaultInspector())
        {
            if (autoUpdate)
            {
                mapGenerator.GenerateMap();
            }
        }

        autoUpdate = EditorGUILayout.Toggle("Auto update", autoUpdate);

        if (GUILayout.Button("Generate"))
        {
            mapGenerator.GenerateMap();
        }
    }
}
