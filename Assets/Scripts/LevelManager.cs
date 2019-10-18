using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    [SerializeField]
    float speedLevelRate = 0.2F;

    Text levelText;
    ItemSpawner itemSpawner;

    float levelInterval = 10.0F; // Enemies will speed up levelInterval seconds before the next level
    float nextLevelTimer = 30.0F;
    /* LevelInterval + nextLevelTimer = entire level length in time */
    float nextLevel = 0.0F;

    bool readyToLevel = false;
    int level;
    int levelUpReward;

    void Start() {
        level = 0;
        levelUpReward = 7;
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
        if (level != 1) 
            FindObjectOfType<ScoreManager>().IncrementScore(levelUpReward * (level - 1), true); // Reward score
        //enemySpawner.DeleteAll(); // Delete all enemies at beginning of new level
        // Could add some music or animation UI here
    }
}
