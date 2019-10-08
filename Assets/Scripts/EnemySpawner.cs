﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField]
    GameObject SharkPrefab;

    [SerializeField]
    GameObject OctopusPrefab;

    ItemSpawner itemSpawner;
    LevelManager levelManager;
    float leftScreenX = -7.0F;
    float leftScreenY1 = 1.40F;
    float leftScreenY2 = -4.15F;
    float rightScreenX = 7.0F;
    float nextSharkSpawn = 0.0F;
    float nextOctopusSpawn = 0.0F; // Will have to change according to levels (at least twice each level)
    float spawnSharkInterval = 20.0F; // Will change according to levels
    float spawnOctopusInterval = 22.0F;
    float sharkAliveTime;
    float octopusAliveTime;
    float currentSpeedGameLevelShark;
    float currentSpeedGameLevelOctopus;

    void Start() {
        sharkAliveTime = 20.0F;
        octopusAliveTime = 10.0F;
        itemSpawner = GameObject.Find("ItemSpawner").GetComponent<ItemSpawner>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        currentSpeedGameLevelShark = 1.8F; // initial speed for sharks
        currentSpeedGameLevelOctopus = 2.3F; // initial speed for octopus
    }

    void Update() {
        currentSpeedGameLevelShark = 1.8F + (float)levelManager.GetLevel() * levelManager.GetSpeedLevelRate();
        currentSpeedGameLevelOctopus = 2.3F + (float)levelManager.GetLevel() * levelManager.GetSpeedLevelRate();
        HandleSharkEnemy();
        HandleOctopusEnemy();
    }

    void HandleSharkEnemy() {
        if (Time.time > nextSharkSpawn) {
            Vector2 spawnedPosition;
            Vector3 spriteDirection;
            nextSharkSpawn = Time.time + spawnSharkInterval;
            float y = Random.Range(leftScreenY1, leftScreenY2);
            float randomSize = Random.Range(1.00F, 3.00F);
            float randomSide = Random.Range(-1, 2); // 0 is left, 1 is right

            if (randomSide == 0) {
                spawnedPosition = new Vector2(leftScreenX, y);
                spriteDirection = Vector3.forward;
            }
            else {
                spawnedPosition = new Vector2(rightScreenX, y);
                spriteDirection = Vector3.back;
            }
            GameObject shark = Instantiate(SharkPrefab, spawnedPosition, Quaternion.LookRotation(spriteDirection)) as GameObject;
            shark.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
            shark.GetComponent<Enemy>().speed = currentSpeedGameLevelShark;
            itemSpawner.enemies.Add(shark);
            Destroy(shark, sharkAliveTime);
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
            GameObject octopus = Instantiate(OctopusPrefab, spawnedPosition, Quaternion.LookRotation(spriteDirection)) as GameObject;
            octopus.GetComponent<Enemy>().speed = currentSpeedGameLevelOctopus;
            itemSpawner.enemies.Add(octopus);
            Destroy(octopus, octopusAliveTime);
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
