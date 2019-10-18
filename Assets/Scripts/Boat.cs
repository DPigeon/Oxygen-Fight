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

    AudioSource collectSound;
    AudioSource bringToBoatSound;

    void Start() {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        scoreManager = GameObject.Find("ScoreText").GetComponent<ScoreManager>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        collectSound = audioSources[0];
        bringToBoatSound = audioSources[1];
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (itemsCollected.Count != 0 && collider.gameObject.name == "Player") {
            bringToBoatSound.Play();
            if (itemsCollected[itemsCollected.Count - 1] == 1) {
                player.ResetSpeed();
                scoreManager.IncrementScore(scoreSmallBar, false);
                itemsCollected.Clear();
                Destroy(items[items.Count - 1]);
            }
            else if (itemsCollected[itemsCollected.Count - 1] == 2) {
                player.ResetSpeed();
                scoreManager.IncrementScore(scoreMediumBar, false);
                itemsCollected.Clear();
                Destroy(items[items.Count - 1]);
            }
            else if (itemsCollected[itemsCollected.Count - 1] == 10) {
                player.ResetSpeed();
                scoreManager.IncrementScore(scoreBag, false);
                itemsCollected.Clear();
                Destroy(items[items.Count - 1]);
            }
        }
    }

    public void AddItem(int itemNumber, GameObject objectItem) {
        itemsCollected.Add(itemNumber);
        items.Add(objectItem);
        collectSound.Play();
    } 
}
