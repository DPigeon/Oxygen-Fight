using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    Text levelText;
    EnemySpawner enemySpawner;

    float levelInterval = 10.0F;
    float nextLevelTimer = 5F;
    float nextLevel = 0.0F;
    int level;

    void Start() {
        level = 0;
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    void Update() {
        if (Time.time > nextLevel) {
            nextLevel = Time.time + nextLevelTimer + levelInterval;
            LevelUp();
            // Octopus has to appear at least twice in a level
            levelText.text = "Level: " + level.ToString("0");
        }
        SpeedUpBeforeLevelUp();
    }

    /* Increase the speed of all enemies before level ends */
    private void SpeedUpBeforeLevelUp() {
        if (nextLevel > 0 && Time.time > nextLevel - levelInterval && enemySpawner.enemies.Count != 0) {
            Debug.Log("hi");
            for (int i = 0; i < enemySpawner.enemies.Count; i++) {
                enemySpawner.enemies[i].GetComponent<Enemy>().IncrementSpeed(10.5F);
            }
        }     
    }

    private void ResetEnemiesSpeed() {
        if (enemySpawner.enemies.Count != 0) {
            for (int i = 0; i < enemySpawner.enemies.Count; i++)
                enemySpawner.enemies[i].GetComponent<Enemy>().ResetSpeed();
        }
    }

    public void LevelUp() {
        level++;
        ResetEnemiesSpeed();
        enemySpawner.DeleteAll(); // Delete all enemies at beginning of new level
        // Could add some music or animation UI here
    }
}
