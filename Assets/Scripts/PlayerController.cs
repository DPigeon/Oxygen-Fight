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
    bool isHurt;
    float hurtDuration = 1.0f;
    float hurtTimer;
    float limit = 6.5F; // Screen limit
    float damagePushForce = 2.5F;
    Vector2 faceDirection;
    Vector3 respawnPosition = new Vector3(0, 2, 0);

    AudioSource hurtSound;
    AudioSource dieSound;

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
            if (hurtTimer >= hurtDuration)
            {
                isHurt = false;
                hurtTimer = 0.0f;
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
                lifeGenerator.RemoveLife();
                transform.position = respawnPosition;
                lifeGenerator.Generate();
                //dieSound.Play();
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
