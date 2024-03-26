using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="NewCrop", menuName = "Crops/Crop")]
public class Crop : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private GameObject crop;
    [SerializeField] private float growTime;
    [SerializeField] private int growStage;
    [SerializeField] private bool harvestable;
    [SerializeField] private GameObject pickupItem;
}
