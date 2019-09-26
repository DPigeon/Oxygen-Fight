using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    Text scoreText;
    Text levelText;
    int score = 0; // Will have to change depending on gold bars
    int level = 1;

    void Start() {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
    }

    void Update()
    {
        scoreText.text = "Score: " + score.ToString("0");
        levelText.text = "Level: " + level.ToString("0");
    }

    public void IncrementScore(int number) {
        score = score + number;
    }

    public void LevelUp() {
        level++;
    }

}
