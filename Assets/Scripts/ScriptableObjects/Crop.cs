using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GrowthStage
{
    public GameObject growthStagePrefab;
    public float growthTime;
}

[CreateAssetMenu(fileName = "New Crop", menuName = "ScriptableObject/Crop")]
public class Crop : ScriptableObject
{
    [Header("Crop Settings")]
    [SerializeField] private int cropID;
    [SerializeField] private string cropName;
    [SerializeField] private GameObject cropPrefab;
    [SerializeField] private Sprite cropIcon;
    [SerializeField] private GameObject cropHarvestPrefab;
    [SerializeField] private GameObject cropSeedPrefab;
    [SerializeField] private int cropPrice;
    [SerializeField] private int cropQuantity;

    [SerializeField] private List<GrowthStage> growthStages = new List<GrowthStage>();
    
    

}

