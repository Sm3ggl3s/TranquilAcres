using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapManager))]
public class MapManagerEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        MapManager mapManager = (MapManager)target;

        if (GUILayout.Button("Generate Voxel Grid")) {
            mapManager.GenerateVoxelGrid();
        }

        if (GUILayout.Button("Clear Voxel Grid")) {
            mapManager.ClearVoxelGrid();
        }
    }
}
