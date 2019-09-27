using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject SmallGoldenBar;
    [SerializeField]
    GameObject MediumGoldenBar;
    [SerializeField]
    GameObject GoldenBag;

    Vector2 spawnedPosition;
    Vector3 rotation;
    float randomX;
    float screenX = 10.5F;
    float spawnRate = 10F;
    float nextSpawn = 0.0F;

    void Start() {    
    }

    void Update() {
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            randomX = Random.Range(-screenX, screenX);
            spawnedPosition = new Vector2(randomX, transform.position.y);
            rotation = new Vector3(0F, 0F, -90.5F);
            Instantiate(SmallGoldenBar, spawnedPosition, Quaternion.Euler(rotation));
        }
    }
}
