using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenItem : MonoBehaviour {
    [SerializeField]
    GameObject SmallGoldenBar = null;
    [SerializeField]
    GameObject MediumGoldenBar = null;
    [SerializeField]
    GameObject GoldenBag = null;
    [SerializeField]
    GameObject NitroTank = null;

    PlayerController player;
    Boat boat;

    Vector3 collectedItemPosition = new Vector3(3.83F, 4.57F, 0.0F);
    Vector2 nitroCollectedPosition = new Vector3(3.00F, 4.57F, 0.0F);
    float speedDecreaseSmallBar = 0.4F;
    float speedDecreaseMediumBar = 0.5F;
    float speedDecreaseBag = 0.8F;
    int scoreSmallBar = 1;
    int scoreMediumBar = 2;
    int scoreBag = 10;

    void Start() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        boat = GameObject.Find("Boat").GetComponent<Boat>();
    }

    /* 
     * If golden item collides with Player, then we display a bar on top (shows that he has it in his bag)
     * We look if our item collected inventory is empty as well
     */
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Player" && boat.itemsCollected.Count == 0) {
            Vector3 rotation = new Vector3(0F, 0F, -90.5F);
            if (gameObject.name == "Small Golden Bar(Clone)") {
                // Display bar on top
                GameObject inventorySmallBar = Instantiate(SmallGoldenBar, collectedItemPosition, Quaternion.Euler(rotation)) as GameObject;

                boat.AddItem(scoreSmallBar, inventorySmallBar);
                player.DecreaseSpeed(speedDecreaseSmallBar);
                Destroy(gameObject);
            }
            if (gameObject.name == "Medium Golden Bar(Clone)") {
                // Display bar on top
                GameObject inventoryMediumBar = Instantiate(MediumGoldenBar, collectedItemPosition, Quaternion.Euler(rotation)) as GameObject;

                boat.AddItem(scoreMediumBar, inventoryMediumBar);
                player.DecreaseSpeed(speedDecreaseMediumBar);
                Destroy(gameObject);
            }
            if (gameObject.name == "Golden Bag(Clone)") {
                // Display bar on top
                GameObject inventoryGoldenBag = Instantiate(GoldenBag, collectedItemPosition, Quaternion.identity) as GameObject;

                boat.AddItem(scoreBag, inventoryGoldenBag);
                player.DecreaseSpeed(speedDecreaseBag);
                Destroy(gameObject);
            }
            if (gameObject.name == "Nitro Tank(Clone)" && player.nitroTankInventory.Count == 0) {
                player.nitroActive = true;
                GameObject nitroTank = Instantiate(NitroTank, nitroCollectedPosition, Quaternion.identity) as GameObject;
                player.nitroTankInventory.Add(nitroTank);
                Destroy(gameObject);
            }
        }
    }
}