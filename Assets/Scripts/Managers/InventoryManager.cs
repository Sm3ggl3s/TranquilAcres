using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManager : MonoBehaviour {

    public static InventoryManager instance;

    [Header("Inventory Settings")]
    public List<Crop> inventoryItems = new List<Crop>();

    [Header("Inventory Display Settings")]
    public TextMeshProUGUI name1;
    public TextMeshProUGUI quantity1;
    public TextMeshProUGUI name2;
    public TextMeshProUGUI quantity2;
    public TextMeshProUGUI name3;
    public TextMeshProUGUI quantity3;

    public int money;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Start() {
        money = 5;

        // Initialize the inventory with 0 quantity for each crop
        foreach (Crop crop in inventoryItems) {
            crop.cropQuantity = 0;
        }
    }

    public void Update() {
        name1.text = inventoryItems[0].cropName;
        quantity1.text = inventoryItems[0].cropQuantity.ToString();
        name2.text = inventoryItems[1].cropName;
        quantity2.text = inventoryItems[1].cropQuantity.ToString();
        name3.text = inventoryItems[2].cropName;
        quantity3.text = inventoryItems[2].cropQuantity.ToString();
    }

    public void AddCrop(Crop crop, int quantityToAdd) {
        // Find the crop in the inventory
        Crop existingCrop = null; // Initialize with a default value
        if (crop.cropName == "Wheat") {
            existingCrop = inventoryItems[0];
        } else if (crop.cropName == "Pumpkin") {
            existingCrop = inventoryItems[1];
        } else if (crop.cropName == "Corn") {
            existingCrop = inventoryItems[2];
        }
        if (existingCrop != null) {
            existingCrop.cropQuantity += quantityToAdd;
            Debug.Log("Added " + quantityToAdd + " " + existingCrop.cropName + " to inventory" + " Total: " + existingCrop.cropQuantity);
        }
    }

    public void SellCrop(Crop crop) {
        Crop existingCrop = null;
        if (crop.cropName == "Wheat") {
            existingCrop = inventoryItems[0];
        } else if (crop.cropName == "Pumpkin") {
            existingCrop = inventoryItems[1];
        } else if (crop.cropName == "Corn") {
            existingCrop = inventoryItems[2];
        }

        if (existingCrop != null) {
            money += existingCrop.cropPrice * existingCrop.cropQuantity;
            existingCrop.cropQuantity = 0;
            Debug.Log("Sold " + existingCrop.cropName + " for " + existingCrop.cropPrice * existingCrop.cropQuantity + " Total Money: " + money);
        }
    }

}
