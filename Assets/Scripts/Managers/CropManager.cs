using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour {
    public static CropManager instance;

    [Header("Crop Settings")]
    [SerializeField] private GameObject startingCropPrefab;
    public Crop cropData;

    [Header("Raycast Settings")]
    [SerializeField] private GameObject rayCastOriginObject;
    [SerializeField] private Transform playerLook;
    
    public LayerMask cropPlotLayer;
    private Ray ray;
    private RaycastHit hit;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
        Transform targetRayCast = rayCastOriginObject.transform.Find("PlayerLook");
        print("Raycast Origin: " + targetRayCast.name);
    }

    private void Start() {
        SetStartingCrop();
    }

    private void Update() {
        ray = new Ray(rayCastOriginObject.transform.position, Vector3.down);
    }

    public void SetStartingCrop() {
        startingCropPrefab = cropData.cropSeedPrefab;
        print("Starting Crop Set: " + cropData.cropName);
    }

    public void PlantCrop(Vector3 targetPosition) {
        Instantiate(startingCropPrefab, targetPosition, Quaternion.identity);
        print("Crop Planted: " + cropData.cropName);
    }

}
