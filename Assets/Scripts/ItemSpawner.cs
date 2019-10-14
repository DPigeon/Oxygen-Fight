using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
    [SerializeField]
    GameObject SmallGoldenBarPrefab = null;
    [SerializeField]
    GameObject MediumGoldenBarPrefab = null;
    [SerializeField]
    GameObject GoldenBagPrefab = null;
    [SerializeField]
    GameObject NitroTankPrefab = null;

    public bool variantSpecial;

    Boat boat;
    Vector2 spawnedPosition;
    Vector3 rotation;
    float randomX;
    float screenX = 6.0F;
    float spawnRate = 2F;
    float nextSpawn = 0.0F;
    float nextNitroSpawn = 0.0F;
    float nitroSpawnRate;
    float randomItem; // Either 0 (small bar), 1 (medium bar) or 2 (bag)
    float aliveSmallBarTime = 9.3F;
    float aliveMediumBarTime = 7.7f;
    float aliveBagTime = 4.5f;
    float aliveNitroTankTime = 4.0f;
    public List<GameObject> enemies = new List<GameObject>(); // Enemies stored here

    void Start() {
        boat = GameObject.Find("Boat").GetComponent<Boat>();
        //variantSpecial = true;
        variantSpecial = ModeSelection.mode;
        VariantVariationSpawn();
    }

    void Update() {
        VariantVariationSpawn();
        if (Time.time > nextSpawn) {
            nextSpawn = Time.time + spawnRate;
            spawnRate = Random.Range(2, 6);
            randomX = Random.Range(-screenX, screenX);
            spawnedPosition = new Vector2(randomX, transform.position.y);
            float randomItem = Random.Range(-1, 3);

            if (randomItem == 0) { 
                rotation = new Vector3(0F, 0F, -90.5F);
                GameObject smallBar = Instantiate(SmallGoldenBarPrefab, spawnedPosition, Quaternion.Euler(rotation)) as GameObject;
                Destroy(smallBar, aliveSmallBarTime); // Destroy within a delay
            }
            else if (randomItem == 1) {
                rotation = new Vector3(0F, 0F, -90.5F);
                GameObject mediumBar = Instantiate(MediumGoldenBarPrefab, spawnedPosition, Quaternion.Euler(rotation)) as GameObject;
                Destroy(mediumBar, aliveMediumBarTime);
            }
            else if (randomItem == 2) {
                spawnedPosition = new Vector2(randomX, transform.position.y + 0.15F);
                GameObject bag = Instantiate(GoldenBagPrefab, spawnedPosition, Quaternion.identity) as GameObject;
                Destroy(bag, aliveBagTime);
            }

            /* Variant Special Version */
            if (variantSpecial) {
                if (Time.time > nextNitroSpawn) {
                    nextNitroSpawn = Time.time + nitroSpawnRate;
                    float x = Random.Range(-screenX, screenX);
                    float y = Random.Range(-4.2F, 1.25F);
                    Vector2 nitroSpawn = new Vector2(x, y);
                    GameObject nitroTank = Instantiate(NitroTankPrefab, nitroSpawn, Quaternion.identity) as GameObject;
                    Destroy(nitroTank, aliveNitroTankTime);
                }
            }
        }
    }

    private void VariantVariationSpawn() {
        if (variantSpecial)
            nitroSpawnRate = Random.Range(3, 5); // Spawns in between 10 to 30 seconds
    }

}
