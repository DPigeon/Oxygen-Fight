using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    [SerializeField]
    GameObject shark;

    float speed; // Will increase after a fixed time hinting towards change of level
    float size;

    float leftScreenRangeX = -11.0F;
    float leftScreenRangeY1 = 1.25F;
    float leftScreenRangeY2 = -4.15F;
    float rightScreenRangeX = 11.0F;
    float nextSpawn = 0.0F;
    float spawnedInterval = 20.0F;

    void Start()
    {
        
    }

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnInterval;
            randomX = Random.Range(-screenX, screenX);
            spawnedPosition = new Vector2(randomX, transform.position.y);
            float randomItem = Random.Range(-1, 3);

            if (randomItem == 0)
            {
                rotation = new Vector3(0F, 0F, -90.5F);
                GameObject smallBar = Instantiate(SmallGoldenBar, spawnedPosition, Quaternion.Euler(rotation)) as GameObject;
                Destroy(smallBar, aliveSmallBarTime); // Destroy within a delay
            }
        }
    }
}
