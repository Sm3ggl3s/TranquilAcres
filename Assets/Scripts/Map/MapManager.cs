using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteInEditMode]
public class MapManager : MonoBehaviour {
    [Header("Map Settings")]
    public GameObject voxelPrefab;
    public int mapWidth = 10;
    public int mapDepth = 10;
    public float voxelSize = 1.0f;

    public void GenerateBaseVoxelGrid() {
        if (voxelPrefab == null) {
            Debug.LogError("Voxel Prefab is not set!");
            return;
        }

        ClearBaseVoxelGrid();

        for (int x = 0; x < mapWidth; x++) {
            for (int z = 0; z < mapDepth; z++) {
                if (voxelPrefab == null) {
                    Debug.LogError("Voxel Prefab became null during the grid generation!");
                    return;
                }
                Vector3 voxelPosition = new Vector3((x-5) * voxelSize, -1, (z-5) * voxelSize);
                Instantiate(voxelPrefab, voxelPosition, Quaternion.identity, transform);
                Debug.Log("Instantiated voxel at: " + voxelPosition);
                
                // Additional debug information
                Debug.Log("voxelPrefab Instance ID: " + voxelPrefab.GetInstanceID());
                Debug.Log("Parent Transform: " + transform.name);
            }
        }
    }

    public void ClearBaseVoxelGrid() {
        while (transform.childCount > 0) {
            Transform child = transform.GetChild(0);
            DestroyImmediate(child.gameObject);
        }
    }

}
