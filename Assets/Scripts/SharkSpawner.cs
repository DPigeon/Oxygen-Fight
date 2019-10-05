using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour {
    [SerializeField]
    GameObject SharkPrefab;

    float speed = 2.0F; // Will increase after a fixed time hinting towards change of level

    float leftScreenX = -11.0F;
    float leftScreenY1 = 1.25F;
    float leftScreenY2 = -4.15F;
    float rightScreenX = 11.0F;
    float nextSpawn = 0.0F;
    float spawnInterval = 20.0F; // Will change according to levels

    void Start() {
        
    }

    void Update() {
        GameObject shark = null;
        float randomSide = -1;
        if (Time.time > nextSpawn) {
            Vector2 spawnedPosition;
            Vector3 spriteDirection;
            nextSpawn = Time.time + spawnInterval;
            float y = Random.Range(leftScreenY1, leftScreenY2);
            float randomSize = Random.Range(1.00F, 3.00F);
            randomSide = Random.Range(-1, 2); // 0 is left, 1 is right

            if (randomSide == 0) {
                spawnedPosition = new Vector2(leftScreenX, y);
                spriteDirection = Vector3.forward;
            }
            else {
                spawnedPosition = new Vector2(rightScreenX, y);
                spriteDirection = Vector3.back;
            }
            shark = Instantiate(SharkPrefab, spawnedPosition, Quaternion.LookRotation(spriteDirection)) as GameObject;
            shark.transform.localScale = new Vector3(randomSize, randomSize, randomSize);
        }
        if (shark != null) {
            SharkMovement(shark, randomSide);
            CheckDestroyBoundaries(shark, randomSide);
        }
    }

    void SharkMovement(GameObject shark, float randomSide) {
        if (randomSide == 0) // Left
            shark.transform.Translate(-Vector2.right * speed * Time.deltaTime);
        else // Right
            shark.transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void CheckDestroyBoundaries(GameObject shark, float spawnedSide) { // Delete the gameObject if its spawned on left side and found on right side (and vice-versa)
        if ((spawnedSide == 0 && transform.position.x == rightScreenX) || (spawnedSide == 1 && transform.position.x == leftScreenX)   )
            Destroy(shark);
    }
}
