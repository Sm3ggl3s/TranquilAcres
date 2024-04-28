using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    // Play game
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Quit game
    public void QuitGame() {
        // For the editor
        Debug.Log("Game Quit");

        // For the build
        Application.Quit();
    }
}
