using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenItem : MonoBehaviour
{
    PlayerController player;
    UIHandler uiHandler;
    Boat boat;

    Vector3 collectedItemPosition = new Vector3(7.0F, 4.5F, 0.0F);
    float speedDecreaseSmallBar = 0.4F;
    float speedDecreaseMediumBar = 0.5F;
    float speedDecreaseBag = 0.8F;
    int scoreSmallBar = 1;
    int scoreMediumBar = 2;
    int scoreBag = 10;

    /* 
     * If golden item collides with Player, then we display a bar on top (shows that he has it in his bag)
     * We look if our item collected inventory is empty as well
     */
    void OnTriggerEnter2D(Collider2D collider) {
        boat = GameObject.Find("Boat").GetComponent<Boat>();
        if (collider.gameObject.name == "Player" && gameObject.name == "Small Golden Bar" && boat.itemsCollected.Count == 0) {

            // Display bar on top
            gameObject.transform.position = collectedItemPosition;

            boat.AddItem(scoreSmallBar);

            // Decrease player speed
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.DecreaseSpeed(speedDecreaseSmallBar);
        }
        if (collider.gameObject.name == "Player" && gameObject.name == "Medium Golden Bar" && boat.itemsCollected.Count == 0) {

            // Display bar on top
            gameObject.transform.position = collectedItemPosition;

            boat.AddItem(scoreMediumBar);

            // Decrease player speed
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.DecreaseSpeed(speedDecreaseMediumBar);
        }
        if (collider.gameObject.name == "Player" && gameObject.name == "Golden Bag" && boat.itemsCollected.Count == 0) {

            // Display bar on top
            gameObject.transform.position = collectedItemPosition;

            boat.AddItem(scoreBag);

            // Decrease player speed
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.DecreaseSpeed(speedDecreaseBag);
        }
    }
}
