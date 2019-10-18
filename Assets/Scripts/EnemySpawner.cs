using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    GameObject SharkPrefab = null;

    [SerializeField]
    GameObject OctopusPrefab = null;

    GameObject currentOctopus;

    ItemSpawner itemSpawner;
    LevelManager levelManager;

    float leftScreenX = -7.0F;
    float leftScreenY1 = 2.20F;
    float leftScreenY2 = -4.15F;
    float rightScreenX = 7.0F;

    float offsetXLeft = 0.0F;
    float offsetXRight = 0.0F;

    float nextSharkSpawn = 0.0F;
    float nextOctopusSpawn = 0.0F;
    float spawnSharkInterval = 10.0F; // Will change according to levels
    float spawnOctopusInterval; // Have to appear twice in a level at random times
    float sharkAliveTime;
    float octopusAliveTime;
    public float currentSpeedGameLevelShark;
    public float currentSpeedGameLevelOctopus;
    public int sharkWaveSize = 3;

    void Start() {
        sharkAliveTime = 20.0F;
        nextOctopusSpawn = Random.Range(1, 10);
        spawnOctopusInterval = Random.Range(12, 20);
        currentOctopus = null;

        itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        currentSpeedGameLevelShark = 1.8F; // initial speed for sharks
        currentSpeedGameLevelOctopus = 2.3F; // initial speed for octopus
    }

    void Update() {
        currentSpeedGameLevelShark = 1.8F + (float)levelManager.GetLevel() * levelManager.GetSpeedLevelRate();
        currentSpeedGameLevelOctopus = 2.3F + (float)levelManager.GetLevel() * levelManager.GetSpeedLevelRate();
        octopusAliveTime = Random.Range(7, 11);
        spawnOctopusInterval = Random.Range(12, 20);
        HandleSharkEnemy();
        HandleOctopusEnemy();
    }

    void HandleSharkEnemy() {
        if (Time.time > nextSharkSpawn) {
            nextSharkSpawn = Time.time + spawnSharkInterval;
            for (int i = 0; i < sharkWaveSize; i++) {
                Vector2 spawnedPosition;
                Vector3 spriteDirection;
                float y = Random.Range(leftScreenY1, leftScreenY2);
                float randomSize = Random.Range(1.00F, 3.00F);
                float randomSide = Random.Range(-1, 2); // 0 is left, 1 is right
                offsetXRight = Random.Range(0, 7);
                offsetXLeft = Random.Range(-7, 0);

                if (randomSide == 0) {
                    spawnedPosition = new Vector2(leftScreenX - Random.Range(0, 7), y);
                    spriteDirection = Vector3.forward;
                }
                else {
                    spawnedPosition = new Vector2(rightScreenX + Random.Range(0, 7), y);
                    spriteDirection = Vector3.back;
                }
                GameObject shark = Instantiate(SharkPrefab, spawnedPosition, Quaternion.LookRotation(spriteDirection)) as GameObject;
                shark.GetComponent<Enemy>().SetSpeed(currentSpeedGameLevelShark);
                shark.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
                itemSpawner.enemies.Add(shark);
                Destroy(shark, Random.Range(sharkAliveTime / 2, sharkAliveTime));
            }
        }
    }

    void HandleOctopusEnemy() {
        if (Time.time > nextOctopusSpawn) {
            Vector2 spawnedPosition;
            Vector3 spriteDirection;
            nextOctopusSpawn = Time.time + spawnOctopusInterval;
            float y = Random.Range(leftScreenY1, leftScreenY2);
            float randomSide = Random.Range(-1, 2); // 0 is left, 1 is right

            if (randomSide == 0) {
                spawnedPosition = new Vector2(leftScreenX, y);
                spriteDirection = Vector3.forward;
            }
            else {
                spawnedPosition = new Vector2(rightScreenX, y);
                spriteDirection = Vector3.back;
            }
            currentOctopus = Instantiate(OctopusPrefab, spawnedPosition, Quaternion.LookRotation(spriteDirection)) as GameObject;
            currentOctopus.GetComponent<Enemy>().SetSpeed(currentSpeedGameLevelOctopus);
            itemSpawner.enemies.Add(currentOctopus);
            Destroy(currentOctopus, octopusAliveTime);
        }
    }

    public void RemoveEnemy(GameObject enemy) {
        int index = itemSpawner.enemies.IndexOf(enemy);
        itemSpawner.enemies.RemoveAt(index);
    }

    public void DeleteAll() {
        if (itemSpawner.enemies.Count != 0) {
            for (int i = 0; i < itemSpawner.enemies.Count; i++) {
                Destroy(itemSpawner.enemies[i]);
            }
            itemSpawner.enemies.Clear();
        }
    }
}
