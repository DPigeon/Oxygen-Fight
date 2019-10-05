using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour {
    [SerializeField]
    GameObject SharkPrefab;

    float leftScreenX = -11.0F;
    float leftScreenY1 = 1.25F;
    float leftScreenY2 = -4.15F;
    float rightScreenX = 11.0F;
    float nextSpawn = 0.0F;
    float spawnInterval = 20.0F; // Will change according to levels

    void Start() {
        
    }

    void Update() {
        if (Time.time > nextSpawn) {
            Vector2 spawnedPosition;
            Vector3 spriteDirection;
            nextSpawn = Time.time + spawnInterval;
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
        }
    }
}
