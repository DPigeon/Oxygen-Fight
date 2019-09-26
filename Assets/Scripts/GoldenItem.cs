using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenItem : MonoBehaviour
{
    PlayerController player;
    UIHandler uiHandler;

    Vector3 collectedItemPosition = new Vector3(7.0F, 4.5F, 0.0F);
    public List<int> itemsCollected = new List<int>(); // To keep track of the elements picked (1, 2 or 10)
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
        if (collider.gameObject.name == "Player" && gameObject.name == "Small Golden Bar" && itemsCollected.Count == 0) {

            // Display bar on top
            gameObject.transform.position = collectedItemPosition;
            itemsCollected.Add(scoreSmallBar);

            // Decrease player speed
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.DecreaseSpeed(speedDecreaseSmallBar);
        }
        if (collider.gameObject.name == "Player" && gameObject.name == "Medium Golden Bar" && itemsCollected.Count == 0)
        {

            // Display bar on top
            gameObject.transform.position = collectedItemPosition;
            itemsCollected.Add(scoreMediumBar);

            // Decrease player speed
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.DecreaseSpeed(speedDecreaseMediumBar);
        }
        if (collider.gameObject.name == "Player" && gameObject.name == "Golden Bag" && itemsCollected.Count == 0)
        {

            // Display bar on top
            gameObject.transform.position = collectedItemPosition;
            itemsCollected.Add(scoreBag);

            // Decrease player speed
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.DecreaseSpeed(speedDecreaseBag);
        }
    }
}
