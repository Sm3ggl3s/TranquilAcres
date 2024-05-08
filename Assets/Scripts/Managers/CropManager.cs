using System.Collections;
using System.Collections.Generic;
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

    private int _nObstacles;

    private void Awake() {
        hasValidPlacement = true;
        isFixed = true;
        _nObstacles = 0;

        _InitializeMaterials();
    }

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

}
