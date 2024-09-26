using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverUI = null;

    bool gameEnded = false;
    AudioSource gameOverSound;

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        gameOverSound = audioSources[0];
    }

    public void EndTheGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            ShowInterface();
        }
    }

    private void ShowInterface()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0.0F; // Freezing the game
        gameOverSound.Play();
    }

    public void LoadNewScene(string name)
    {
        SceneManager.LoadScene(name);
        Time.timeScale = 1.0F;
    }
}
