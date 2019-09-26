using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenItem : MonoBehaviour
{
    PlayerController player;
    UIHandler uiHandler;

    Vector3 collectedItemPosition = new Vector3(7.0F, 4.5F, 0.0F);
    float speedDecreaseSmallBar = 0.4F;
    float speedDecreaseMediumBar = 0.5F;
    float speedDecreaseBag = 0.8F;
    int scoreSmallBar = 1;
    int scoreMediumBar = 2;
    int scoreBag = 10;

    /* 
     * If golden item collides with Player, then we display a bar on top (shows that he has it in his bag)
     */
    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.name == "Player") {

            // Display bar on top
            gameObject.transform.position = collectedItemPosition;

            // Decrease player speed
            player = GameObject.Find("Player").GetComponent<PlayerController>();
            player.DecreaseSpeed(speedDecreaseSmallBar);

            // Add to counter
            uiHandler = GameObject.Find("ScoreText").GetComponent<UIHandler>();
            uiHandler.IncrementScore(scoreSmallBar);
        }
    }
}
