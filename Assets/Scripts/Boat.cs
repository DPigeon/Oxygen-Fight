using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    GoldenItem item;
    PlayerController player;
    UIHandler uiHandler;

    public List<int> itemsCollected = new List<int>(); // To keep track of the elements picked (1, 2 or 10)
    int scoreSmallBar = 1;
    int scoreMediumBar = 2;
    int scoreBag = 10;

    void OnTriggerEnter2D(Collider2D collider) {
        if (itemsCollected.Count != 0 && collider.gameObject.name == "Player") {
            if (itemsCollected[itemsCollected.Count - 1] == 1) {
                item = GameObject.Find("Small Golden Bar").GetComponent<GoldenItem>();
                // Put back player's speed to normal
                player = GameObject.Find("Player").GetComponent<PlayerController>();
                player.ResetSpeed();

                // Add to counter
                uiHandler = GameObject.Find("ScoreText").GetComponent<UIHandler>();
                uiHandler.IncrementScore(scoreSmallBar);

                itemsCollected.Clear();
                Destroy(GameObject.Find("Small Golden Bar"));
            }
            else if (itemsCollected[itemsCollected.Count - 1] == 2) {
                item = GameObject.Find("Medium Golden Bar").GetComponent<GoldenItem>();
                // Put back player's speed to normal
                player = GameObject.Find("Player").GetComponent<PlayerController>();
                player.ResetSpeed();

                // Add to counter
                uiHandler = GameObject.Find("ScoreText").GetComponent<UIHandler>();
                uiHandler.IncrementScore(scoreMediumBar);

                itemsCollected.Clear();
                Destroy(GameObject.Find("Medium Golden Bar"));
            }
            else if (itemsCollected[itemsCollected.Count - 1] == 10) {
                item = GameObject.Find("Golden Bag").GetComponent<GoldenItem>();
                // Put back player's speed to normal
                player = GameObject.Find("Player").GetComponent<PlayerController>();
                player.ResetSpeed();

                // Add to counter
                uiHandler = GameObject.Find("ScoreText").GetComponent<UIHandler>();
                uiHandler.IncrementScore(scoreBag);

                itemsCollected.Clear();
                Destroy(GameObject.Find("Golden Bag"));
            }
        }
    }

    public void AddItem(int itemNumber) {
        itemsCollected.Add(itemNumber);
    } 

}
