using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    Text scoreText;
    int score = 0;

    void Start() {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    void Update() {
        scoreText.text = "Score: " + score.ToString("0");
    }

    public void IncrementScore(int number) {
        score = score + number;
    }
}
