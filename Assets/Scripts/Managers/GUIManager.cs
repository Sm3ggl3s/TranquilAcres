using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    public static GUIManager instance;

    public static bool isGamePaused = false;
    
    [Header("GUI Settings")]
    public GameObject InventoryPanel;
    public GameObject CropToolBar;
    public GameObject BuildToolBar;
    public GameObject PauseMenu;


    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isGamePaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        PauseMenu.SetActive(true);
        InventoryPanel.SetActive(false);
        CropToolBar.SetActive(false);
        BuildToolBar.SetActive(false);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void ResumeGame() {
        PauseMenu.SetActive(false);
        InventoryPanel.SetActive(true);
        CropToolBar.SetActive(true);
        BuildToolBar.SetActive(true);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void Menu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
