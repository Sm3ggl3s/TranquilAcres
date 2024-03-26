using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/item")]
public class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite image;
    [SerializeField] private ItemType type;
    [SerializeField] private int quantity;
}

public enum ItemType {
    Tool,
    Crop,
    Seed,
    Building
}

