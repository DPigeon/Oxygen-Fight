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
    Vector3 respawnPosition = new Vector3(0, 2, 0);

    Animator animator;
    Rigidbody2D rigidBody2D;
    Transform spriteChild;
    LifeGenerator lifeGenerator;

    void Start() {
       /*
        * We get the components made in the player's inspector
        */
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteChild = transform.Find("PlayerSprite");
        lifeGenerator = GameObject.Find("LifeGenerator").GetComponent<LifeGenerator>();
    }

    void Update() {
        ControlCharacter();

        animator.SetBool("isSwimming", isSwimming);
        animator.SetBool("isSwimmingFast", isSwimmingFast);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Shark" || collider.gameObject.name == "Octopus") {
            // Create some animation of hurting here & sound later
            if (lifeGenerator.lives.Count == 2) {
                lifeGenerator.RemoveLife();
            }
            else if (lifeGenerator.lives.Count == 1) {
                lifeGenerator.RemoveLife();
                transform.position = respawnPosition;
                lifeGenerator.Generate();
                // Make player drawn animation here later
            }
        }
    }

    private void ControlCharacter() {
        /* Speed Character */
        float speed = isSwimmingFast ? swimFastSpeed : swimSpeed;

        /* Swimming Fast */
        if (Input.GetButtonDown("Run")) isSwimmingFast = true;
        if (Input.GetButtonUp("Run")) isSwimmingFast = false;

        isSwimming = false;

        if (Input.GetButton("Up")) {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            SpriteDirectionUp(Vector2.up); 
            isSwimming = true;
        }
        if (Input.GetButton("Down")) {
            transform.Translate(-Vector2.up * speed * Time.deltaTime);
            SpriteDirectionUp(-Vector2.up); 
            isSwimming = true;
        }
        if (Input.GetButton("Left")) {
            transform.Translate(-Vector2.right * speed * Time.deltaTime);
            SpriteDirectionRight(-Vector2.right);
            isSwimming = true;
        }
        if (Input.GetButton("Right")) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            SpriteDirectionRight(Vector2.right);
            isSwimming = true;
        }

        /* Reseting animation to idle if not swimming */
        if (!isSwimming) SpriteDirectionRight(Vector2.right);
    }

    /* 
     * Sprite Directions (up, down & right, left)
     */
    private void SpriteDirectionUp(Vector2 direction) {
        Quaternion rotation3D = direction == Vector2.up ? Quaternion.LookRotation(Vector3.back, Vector3.right) : Quaternion.LookRotation(Vector3.back, Vector3.left);
        spriteChild.rotation = rotation3D;
    }

    private void SpriteDirectionRight(Vector2 direction) {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        spriteChild.rotation = rotation3D;
    }

    public void DecreaseSpeed(float number) {
        swimSpeed = swimSpeed - number;
        swimFastSpeed = swimFastSpeed - number;
    }

    public void ResetSpeed() {
        swimSpeed = 0.9F;
        swimFastSpeed = 1.5F;
    }
}
