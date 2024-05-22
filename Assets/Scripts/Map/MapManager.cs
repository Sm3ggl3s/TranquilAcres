using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    [Header("Map Settings")]
    public GameObject voxelPrefab;
    public int mapWidth = 10;
    public int mapDepth = 10;
    public float voxelSize = 1.0f;

    private void Start() {
        Debug.Log("Start() called");
        if (voxelPrefab == null) {
            Debug.LogError("Voxel Prefab is not assigned in the Inspector.");
        }
        GenerateVoxelGrid();
    }

    public void GenerateVoxelGrid() {
        if (voxelPrefab == null) {
            Debug.LogError("Voxel Prefab is not set!");
            return;
        }
        for (int x = 0; x < mapWidth; x++) {
            for (int z = 0; z < mapDepth; z++) {
                if (voxelPrefab == null) {
                    Debug.LogError("Voxel Prefab became null during the grid generation!");
                    return;
                }
                Vector3 voxelPosition = new Vector3((x-5) * voxelSize, 0, (z-5) * voxelSize);
                Instantiate(voxelPrefab, voxelPosition, Quaternion.identity, transform);
                Debug.Log("Instantiated voxel at: " + voxelPosition);
                
                // Additional debug information
                Debug.Log("voxelPrefab Instance ID: " + voxelPrefab.GetInstanceID());
                Debug.Log("Parent Transform: " + transform.name);
            }
        }
    }

}
