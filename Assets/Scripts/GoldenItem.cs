using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenItem : MonoBehaviour {
    PlayerController player;
    Boat boat;

    Vector3 collectedItemPosition = new Vector3(7.0F, 4.5F, 0.0F);
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
            if (gameObject.name == "Small Golden Bar(Clone)") {
                // Display bar on top
                gameObject.transform.position = collectedItemPosition;
    
                boat.AddItem(scoreSmallBar, gameObject);
                player.DecreaseSpeed(speedDecreaseSmallBar);
            }
            if (gameObject.name == "Medium Golden Bar(Clone)") {
                // Display bar on top
                gameObject.transform.position = collectedItemPosition;

                boat.AddItem(scoreMediumBar, gameObject);
                player.DecreaseSpeed(speedDecreaseMediumBar);
            }
            if (gameObject.name == "Golden Bag(Clone)") {
                // Display bar on top
                gameObject.transform.position = collectedItemPosition;

                boat.AddItem(scoreBag, gameObject);
                player.DecreaseSpeed(speedDecreaseBag);
            }
        }
    }
}
