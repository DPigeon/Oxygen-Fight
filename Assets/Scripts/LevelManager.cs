using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    Text levelText;
    EnemySpawner enemySpawner;

    float levelInterval = 10.0F;
    float nextLevelTimer = 30F;
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
    }

    /* Increase the speed of all enemies before level ends */
    private void SpeedUpBeforeLevelUp() {
        if (Time.time > nextLevel - levelInterval && enemySpawner.enemies.Count != 0) {
            for (int i = 0; i < enemySpawner.enemies.Count; i++) {
                //enemySpawner.enemies[i].GetComponent<Shark>().IncrementSpeed(0.5);
            }
        }     
    }

    private void ResetEnemiesSpeed() {
        if (enemySpawner.enemies.Count != 0) {
            /*for (int i = 0; i < enemySpawner.enemies.Count; i++)
                enemySpawner.enemies[i].ResetSpeed;*/
        }
    }

    public void LevelUp() {
        level++;

        enemySpawner.DeleteAll(); // Delete all enemies at beginning of new level
        // Could add some music or animation UI here
    }
}
