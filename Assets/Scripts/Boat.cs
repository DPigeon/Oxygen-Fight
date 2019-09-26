using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    GoldenItem item;
    PlayerController player;
    UIHandler uiHandler;

    int scoreSmallBar = 1;
    int scoreMediumBar = 2;
    int scoreBag = 10;

    void OnTriggerEnter2D(Collider2D collider)
    {
        item = GameObject.Find("Small Golden Bar").GetComponent<GoldenItem>();
        if (item.itemsCollected.Count != 0)
        {
            if (collider.gameObject.name == "Player" && item.itemsCollected[item.itemsCollected.Count - 1] == 1)
            {
                // Put back player's speed to normal
                player = GameObject.Find("Player").GetComponent<PlayerController>();
                player.ResetSpeed();

                // Add to counter
                uiHandler = GameObject.Find("ScoreText").GetComponent<UIHandler>();
                uiHandler.IncrementScore(scoreSmallBar);

                //Destroy(item);
            }
            if (collider.gameObject.name == "Player" && item.itemsCollected[item.itemsCollected.Count - 1] == 2)
            {
                // Put back player's speed to normal
                player = GameObject.Find("Player").GetComponent<PlayerController>();
                player.ResetSpeed();

                // Add to counter
                uiHandler = GameObject.Find("ScoreText").GetComponent<UIHandler>();
                uiHandler.IncrementScore(scoreMediumBar);

                //Destroy(item);
            }
            if (collider.gameObject.name == "Player" && item.itemsCollected[item.itemsCollected.Count - 1] == 10)
            {
                // Put back player's speed to normal
                player = GameObject.Find("Player").GetComponent<PlayerController>();
                player.ResetSpeed();

                // Add to counter
                uiHandler = GameObject.Find("ScoreText").GetComponent<UIHandler>();
                uiHandler.IncrementScore(scoreBag);

                //Destroy(item);
            }
        }
    }
}
