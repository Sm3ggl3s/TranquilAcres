using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GrowthStage {
    public GameObject growthStagePrefab;
    public float growthTime;
}

[CreateAssetMenu(fileName = "New Crop", menuName = "ScriptableObject/Crop")]
public class Crop : ScriptableObject {
    [Header("Crop Settings")]
    public int cropID;
    public string cropName;
    public GameObject cropSeedPrefab;
    public Sprite cropIcon;
    public GameObject cropHarvestPrefab;
    public int cropPrice;
    public int cropQuantity;
    public int cropQuantityToAdd;
    public List<GrowthStage> growthStages = new List<GrowthStage>();
    
}

