using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {
    GoldenItem item;
    PlayerController player;
    ScoreManager scoreManager;

    public List<int> itemsCollected = new List<int>(); // To keep track of the elements picked (1, 2 or 10)
    public List<GameObject> items = new List<GameObject>(); // Keep track of the GoldenItem ID in inventory
    int scoreSmallBar = 1;
    int scoreMediumBar = 2;
    int scoreBag = 10;

    void Start() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreManager = GameObject.Find("ScoreText").GetComponent<ScoreManager>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (itemsCollected.Count != 0 && collider.gameObject.name == "Player") {
            if (itemsCollected[itemsCollected.Count - 1] == 1) {
                player.ResetSpeed();
                scoreManager.IncrementScore(scoreSmallBar);
                itemsCollected.Clear();
                Destroy(items[items.Count - 1]);
            }
            else if (itemsCollected[itemsCollected.Count - 1] == 2) {
                player.ResetSpeed();
                scoreManager.IncrementScore(scoreMediumBar);
                itemsCollected.Clear();
                Destroy(items[items.Count - 1]);
            }
            else if (itemsCollected[itemsCollected.Count - 1] == 10) {
                player.ResetSpeed();
                scoreManager.IncrementScore(scoreBag);
                itemsCollected.Clear();
                Destroy(items[items.Count - 1]);
            }
        }
    }

    public void AddItem(int itemNumber, GameObject objectItem) {
        itemsCollected.Add(itemNumber);
        items.Add(objectItem);
    } 
}
