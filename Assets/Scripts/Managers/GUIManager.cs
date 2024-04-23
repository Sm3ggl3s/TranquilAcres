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
    public GameObject buildingMenuPanel;


    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }

        isInventoryActive = false;
        isBuildingMenuActive = false;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            ShowInventory();
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            ShowBuildingMenu();
        }
    }

    private void ShowInventory() {
        inventoryPanel.SetActive(!isInventoryActive);
        isInventoryActive = !isInventoryActive;

        // Disable building menu if inventory is active
        if (isBuildingMenuActive) {
            buildingMenuPanel.SetActive(false);
        }
        print("Inventory Active: " + isInventoryActive);
    }
    
    private void ShowBuildingMenu() {
        buildingMenuPanel.SetActive(!isBuildingMenuActive);
        isBuildingMenuActive = !isBuildingMenuActive;

        // Disable inventory if building menu is active
        if (isInventoryActive) {
            inventoryPanel.SetActive(false);
        }
        print("Building Menu Active: " + isBuildingMenuActive);
    }
}
