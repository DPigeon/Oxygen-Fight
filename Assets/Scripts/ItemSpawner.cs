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

    Boat boat;
    Vector2 spawnedPosition;
    Vector3 rotation;
    float randomX;
    float screenX = 10.5F;
    float spawnRate = 2F;
    float nextSpawn = 0.0F;
    float randomItem; // Either 0 (small bar), 1 (medium bar) or 2 (bag)
    float aliveSmallBarTime = 9.3F;
    float aliveMediumBarTime = 7.7f;
    float aliveBagTime = 4.5f;

    void Start() {
        boat = GameObject.Find("Boat").GetComponent<Boat>();
    }

    void Update() {
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            randomX = Random.Range(-screenX, screenX);
            spawnedPosition = new Vector2(randomX, transform.position.y);
            float randomItem = Random.Range(-1, 3);

            if (randomItem == 0) { 
                rotation = new Vector3(0F, 0F, -90.5F);
                GameObject smallBar = Instantiate(SmallGoldenBar, spawnedPosition, Quaternion.Euler(rotation)) as GameObject;
                Destroy(smallBar, aliveSmallBarTime); // Destroy within a delay
            }
            else if (randomItem == 1) {
                rotation = new Vector3(0F, 0F, -90.5F);
                GameObject mediumBar = Instantiate(MediumGoldenBar, spawnedPosition, Quaternion.Euler(rotation)) as GameObject;
                Destroy(mediumBar, aliveMediumBarTime);
            }
            else if (randomItem == 2) {
                spawnedPosition = new Vector2(randomX, transform.position.y + 0.15F);
                GameObject bag = Instantiate(GoldenBag, spawnedPosition, Quaternion.identity) as GameObject;
                Destroy(bag, aliveBagTime);
            }
            // Don't destroy if in inventory (DEFECT: once the destroy timer finishes, the gameObject destroys in inventory)
            
        }
    }
}
