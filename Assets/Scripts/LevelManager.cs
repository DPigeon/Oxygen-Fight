using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    Text levelText;
    ItemSpawner itemSpawner;

    float levelInterval = 10.0F; // Enemies will speed up 10 seconds before the next level
    float nextLevelTimer = 5.0F;
    /* LevelInterval + nextLevelTimer = entire level length in time */
    float nextLevel = 0.0F;
    float speedLevelRate = 0.2F;
    float speedIncrement = 0.001F;
    int level;

    void Start() {
        level = 0;
        levelText = GameObject.Find("LevelText").GetComponent<Text>();
        itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();
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
        if (Time.time > nextLevel - levelInterval && Time.time < nextLevel) {
            if (itemSpawner.enemies.Count != 0) {
                for (int i = 0; i < itemSpawner.enemies.Count; i++) {
                    float speed = itemSpawner.enemies[i].GetComponent<Enemy>().GetSpeed();
                    itemSpawner.enemies[i].GetComponent<Enemy>().ChangeSpeed(speed + speedIncrement);
                }
            }
        }     
    }

    private void IncrementSpeed() { // Increment speed a bit after each level
        if (itemSpawner.enemies.Count != 0) {
            for (int i = 0; i < itemSpawner.enemies.Count; i++)
                itemSpawner.enemies[i].GetComponent<Enemy>().IncrementSpeed(speedLevelRate);
        }
    }

    public int GetLevel() {
        return level;
    }

    public float GetSpeedLevelRate()  {
        return speedLevelRate;
    }

    public void LevelUp() {
        level++;
        IncrementSpeed();
        //enemySpawner.DeleteAll(); // Delete all enemies at beginning of new level
        // Could add some music or animation UI here
    }
}
