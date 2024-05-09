using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour {
    public static ShopManager instance;
   
    InventoryManager inv;

    [Header("Shop Settings")]
    public TextMeshProUGUI moneyText;   
    
    private void Start() {
        inv = InventoryManager.instance;
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Update() {
        moneyText.text = "$ " + inv.money;
    }
    

}
