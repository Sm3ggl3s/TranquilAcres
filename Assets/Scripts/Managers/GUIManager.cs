using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public static GUIManager instance;
    private bool isInventoryActive;
    private bool isBuildingMenuActive;

    [Header("Inventory Settings")]
    public GameObject inventoryPanel;


    [Header("Building Menu Settings")]
    

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }

        isInventoryActive = false;
        isBuildingMenuActive = false;
    }

    private void ShowInventory() {

    }
    }
}
