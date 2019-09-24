using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /* 
     * Initializing all our variables
     */
    [SerializeField]
    float swimSpeed;

    [SerializeField]
    float swimFastSpeed;

    bool isSwimming;
    bool isSwimmingFast;
    bool isIdle;

    Animator animator;
    Rigidbody2D rigidBody2D;
    Transform spriteChild;
   
    void Start()
    {
        /*
         * We get the components made in the player's inspector
         */
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteChild = transform.Find("PlayerSprite");
    }

    
    void Update()
    {
        
    }
}
