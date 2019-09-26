using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenItem : MonoBehaviour
{
    /* 
     * If golden item collides with Player, then we display a bar on top (shows that he has it in his bag)
     */
    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.name == "Player")
        {
            Destroy(gameObject); // Destroy the bar
            // Decrease player speed
            // Display bar on top as well
            // Add to counter + 1
        }
    }
}
