using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverUI;

    bool gameEnded = false;

    public void EndTheGame() {
        if (!gameEnded) {
            gameEnded = true;
            ShowInterface();
        }
    }

    private void ShowInterface() {
        gameOverUI.SetActive(true);
        Time.timeScale = 0.0F; // Freezing the game
    }

    public void LoadNewScene(string name) {
        SceneManager.LoadScene(name);
        Time.timeScale = 1.0F;
    }
}
