using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    /* 
     * Initializing all our variables
     */
    [SerializeField]
    GameObject DieMorph = null;

    [SerializeField]
    float swimSpeed;

    [SerializeField]
    float swimFastSpeed;

    bool isSwimming;
    bool isSwimmingFast;
    bool isHurt;

    bool dead;

    float deadTimer;
    float deadDuration = 2.0F;

    float hurtDuration = 1.5f;
    float hurtTimer;
    float limit = 6.5F; // Screen limit
    Vector2 faceDirection;
    Vector3 respawnPosition = new Vector3(0, 1.8F, 0);

    AudioSource hurtSound;
    AudioSource dieSound;

    Animator animator;
    Rigidbody2D rigidBody2D;
    Transform spriteChild;
    LifeGenerator lifeGenerator;
    Boat boat;

    void Start() {
        /*
         * We get the components made in the player's inspector
         */
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteChild = transform.Find("PlayerSprite");
        lifeGenerator = GameObject.Find("LifeGenerator").GetComponent<LifeGenerator>();
        boat = GameObject.Find("Boat").GetComponent<Boat>();

        // Audio
        AudioSource[] audioSources = GetComponents<AudioSource>();
        hurtSound = audioSources[0];
        dieSound = audioSources[1];
    }

    void Update() {
        ControlCharacter();
        CheckBoundaries();

        animator.SetBool("isSwimming", isSwimming);
        animator.SetBool("isSwimmingFast", isSwimmingFast);
        animator.SetBool("isHurt", isHurt);

        if (isHurt) {
            hurtTimer += Time.deltaTime;
            if (hurtTimer >= hurtDuration) {
                isHurt = false;
                hurtTimer = 0.0f;
            }
        }

        if (dead) {
            deadTimer += Time.deltaTime;
            Debug.Log(Time.deltaTime);
            if (deadTimer > deadDuration) {
                dead = false;
                deadTimer = 0.0f;
                transform.localScale = new Vector3(1.646864F, 1.693038F, 1.2313F); // Respawn
                transform.position = respawnPosition;
                lifeGenerator.Generate();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Shark(Clone)" || collider.gameObject.name == "Octopus(Clone)") {
            // Create some animation of hurting here & sound later
            if (lifeGenerator.lives.Count == 2) {
                lifeGenerator.RemoveLife();
                isHurt = true;
                //hurtSound.Play();
            }
            else if (lifeGenerator.lives.Count == 1) {
                if (boat.items.Count != 0)
                    Destroy(boat.items[boat.items.Count - 1]); // If any item in player inventory, destroy
                lifeGenerator.RemoveLife();
                Die();
                //lifeGenerator.Generate();
                //dieSound.Play();
                // Make player drawn animation here later
            }
        }
    }

    private void Die() {
        dead = true;
        transform.localScale = new Vector3(0, 0, 0); // Hide player (deleted and dead)
        GameObject morph = Instantiate(DieMorph, transform.position, Quaternion.identity) as GameObject;
        Destroy(morph, deadDuration);
        // After 2 seconds, load main menu here later
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

    private void CheckBoundaries() {
        if (transform.position.x < -limit)
        {
            transform.position = new Vector2(limit, transform.position.y);
        }
        else if (transform.position.x > limit)
        {
            transform.position = new Vector2(-limit, transform.position.y);
        }
    }

    /* 
     * Sprite Directions (up, down & right, left)
     */
    private void SpriteDirectionUp(Vector2 direction) {
        faceDirection = direction;
        Quaternion rotation3D = direction == Vector2.up ? Quaternion.LookRotation(Vector3.back, Vector3.right) : Quaternion.LookRotation(Vector3.back, Vector3.left);
        spriteChild.rotation = rotation3D;
    }

    private void SpriteDirectionRight(Vector2 direction) {
        faceDirection = direction;
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
