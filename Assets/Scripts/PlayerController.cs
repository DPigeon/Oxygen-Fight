using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    /* 
     * Initializing all our variables
     */
    [SerializeField]
    float swimSpeed;

    [SerializeField]
    float swimFastSpeed;

    bool isSwimming;
    bool isSwimmingFast;

    Animator animator;
    Rigidbody2D rigidBody2D;
    Transform spriteChild;
   
    void Start() {
       /*
        * We get the components made in the player's inspector
        */
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteChild = transform.Find("PlayerSprite");
    }

    void Update() {
        ControlCharacter();

        animator.SetBool("isSwimming", isSwimming);
        animator.SetBool("isSwimmingFast", isSwimmingFast);

    }

    private void ControlCharacter() {
        /* Speed Character */
        float speed = isSwimmingFast ? swimFastSpeed : swimSpeed;

        /* Swimming Fast */
        if (Input.GetButtonDown("Run")) isSwimmingFast = true;
        if (Input.GetButtonUp("Run")) isSwimmingFast = false;

        isSwimming = false;

        if (Input.GetButton("Left")) {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
            SpriteDirection(-Vector2.right);
            isSwimming = true;
        }
        if (Input.GetButton("Right")) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            SpriteDirection(Vector2.right);
            isSwimming = true;
        }
    }

    private void SpriteDirection(Vector2 direction) {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        spriteChild.rotation = rotation3D;
    }
}
