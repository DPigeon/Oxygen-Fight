using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    Text scoreText;
    public int score = 0;

    AudioSource levelUpSound;

    void Start() {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        levelUpSound = audioSources[0];
    }

    void Update() {
        scoreText.text = "Score: " + score.ToString("0");
    }

    public void IncrementScore(int number, bool levelUp) {
        score = score + number;
        if (levelUp)
            levelUpSound.Play();
    }
}
