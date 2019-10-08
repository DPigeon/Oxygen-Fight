using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    Text levelText;
    ItemSpawner itemSpawner;

    float levelInterval = 10.0F; // Enemies will speed up 10 seconds before the next level
    float nextLevelTimer = 5.0F;
    /* LevelInterval + nextLevelTimer = entire level length in time */
    float nextLevel = 0.0F;
    float speedLevelRate = 1.2F;

    bool readyToLevel = false;
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
        else if (Time.time > nextLevel - levelInterval && Time.time < nextLevel && !readyToLevel) {
            SpeedUpBeforeLevelUp();
        }   
    }

    /* Increase the speed of all enemies before level ends */
    private void SpeedUpBeforeLevelUp() {
        if (itemSpawner.enemies.Count != 0) {
            for (int i = 0; i < itemSpawner.enemies.Count; i++) {
                float speed = itemSpawner.enemies[i].GetComponent<Enemy>().GetSpeed();
                itemSpawner.enemies[i].GetComponent<Enemy>().IncrementSpeed(speedLevelRate);
            }
        }
        readyToLevel = true;
    }

    public int GetLevel() {
        return level;
    }

    public float GetSpeedLevelRate()  {
        return speedLevelRate;
    }

    public void LevelUp() {
        readyToLevel = false;
        level++;
        //enemySpawner.DeleteAll(); // Delete all enemies at beginning of new level
        // Could add some music or animation UI here
    }
}
