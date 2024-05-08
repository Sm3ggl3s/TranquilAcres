using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CropPlacementMode {
    Fixed,
    Valid,
    Invalid

}

public class CropManager : MonoBehaviour {

    [Header("Crop Settings")]
    [SerializeField] private GameObject startingCropPrefab;
    public Crop cropData;

    [Header("Crop Material Settings")]
    public Material validPlacementMaterial;
    public Material invalidPlacementMaterial;

    public MeshRenderer[] meshComponents;
    private Dictionary<MeshRenderer, List<Material>> initialMaterials;

    [HideInInspector] public bool hasValidPlacement;
    [HideInInspector] public bool isFixed;

    public GameObject growthStagePrefab = null;
    public bool isHarvestable = false;

    private int _nObstacles;

    private void Awake() {
        hasValidPlacement = true;
        isFixed = true;
        _nObstacles = 0;

        _InitializeMaterials();
    }

    private void Update() {
        Debug.Log("Harvestable Update method: " + isHarvestable);
        Debug.Log("Grow Stage Prefab: " + growthStagePrefab);

        if (isHarvestable && Input.GetKeyDown(KeyCode.E)) {
            HarvestCrop();
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (isFixed) {
            GetComponent<Collider>().isTrigger = false;
            return;
        }

        // ignore Crop Plot Objects
        if (_IsCropPlot(other.gameObject)) {
            return;
        }

        _nObstacles++;
        SetCropPlacementMode(PlacementMode.Invalid);
    }

    private void OnTriggerExit(Collider other) {
        if (isFixed) {
            return;
        }

        // ignore Crop Plot Objects
        if (_IsCropPlot(other.gameObject)) {
            return;
        }

        _nObstacles--;
        if (_nObstacles == 0) {
            SetCropPlacementMode(PlacementMode.Valid);
        }


        // if the player is colliding with the building
        if (other.gameObject.CompareTag("Player")) {
            GetComponent<Collider>().isTrigger = true;
        }
    }

    #region Placement

    public void SetCropPlacementMode(PlacementMode mode) {
        if (mode == PlacementMode.Fixed) {
            isFixed = true;
            hasValidPlacement = false;
        } else if (mode == PlacementMode.Valid) {
            hasValidPlacement = true;
        } else if (mode == PlacementMode.Invalid) {
            hasValidPlacement = false;
        }
        SetMaterial(mode);
    }

    public void SetMaterial(PlacementMode mode) {
        if (mode == PlacementMode.Fixed) {
            foreach (MeshRenderer r in meshComponents)
                r.sharedMaterials = initialMaterials[r].ToArray();
        } else {
            Material matToApply = mode == PlacementMode.Valid
                ? validPlacementMaterial : invalidPlacementMaterial;

            Material[] m; 
            int nMaterials;
            foreach (MeshRenderer r in meshComponents) {
                nMaterials = initialMaterials[r].Count;
                m = new Material[nMaterials];
                for (int i = 0; i < nMaterials; i++)
                    m[i] = matToApply;
                r.sharedMaterials = m;
            }
        }
    }
    private void _InitializeMaterials() {
        if (initialMaterials == null)
            initialMaterials = new Dictionary<MeshRenderer, List<Material>>();
        if (initialMaterials.Count > 0) {
            foreach (var l in initialMaterials) l.Value.Clear();
            initialMaterials.Clear();
        }

        foreach (MeshRenderer r in meshComponents) {
            initialMaterials[r] = new List<Material>(r.sharedMaterials);
        }
    }

    private bool _IsCropPlot(GameObject o) {
        return ((1 << o.layer) & CropPlacer.instance.cropPlotLayer.value) != 0;
    }

    #endregion

    public IEnumerator GrowCrop() {
        // Hide the initial startingCropPrefab
        startingCropPrefab.GetComponent<MeshRenderer>().enabled = false;

        // Iterate through growth stages
        for (int i = 0; i < cropData.growthStages.Count; i++) {
            // Destroy the previous spawned crop prefab
            if (growthStagePrefab != null) {
                Destroy(growthStagePrefab);
            }

            GameObject currentStage = cropData.growthStages[i].growthStagePrefab;
            float currentGrowthTime = cropData.growthStages[i].growthTime;

            growthStagePrefab = Instantiate(currentStage, transform.position, Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));


            // If it's not the first growth stage, deactivate the startingCropPrefab
            if (i > 0) {
                startingCropPrefab.GetComponent<MeshRenderer>().enabled = false;
            }

            // Wait for growth time
            yield return new WaitForSeconds(currentGrowthTime);
        }
        isHarvestable = true;
    }



    public void HarvestCrop() {
        // Destroy the current growth stage prefab
        Destroy(growthStagePrefab);

        GameObject harvestPrefab = cropData.cropHarvestPrefab;
        Instantiate(harvestPrefab, transform.position + Vector3.forward, Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }



}
