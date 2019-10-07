using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {
    Text levelText;
    ItemSpawner itemSpawner;

    float levelInterval = 10.0F;
    float nextLevelTimer = 5F;
    float nextLevel = 0.0F;
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
        if (Time.time > nextLevel - levelInterval) {
            if (itemSpawner.enemies.Count != 0) {
                for (int i = 0; i < itemSpawner.enemies.Count; i++) {
                    itemSpawner.enemies[i].GetComponent<Enemy>().ChangeSpeed(3.5F);
                }
            }
        }     
    }

    private void ResetEnemiesSpeed() {
        if (itemSpawner.enemies.Count != 0) {
            for (int i = 0; i < itemSpawner.enemies.Count; i++)
                itemSpawner.enemies[i].GetComponent<Enemy>().ResetSpeed();
        }
    }

    public void LevelUp() {
        level++;
        ResetEnemiesSpeed();
        //enemySpawner.DeleteAll(); // Delete all enemies at beginning of new level
        // Could add some music or animation UI here
    }
}
