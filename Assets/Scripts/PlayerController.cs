using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    /* 
     * Initializing all our variables
     */
    [SerializeField]
    GameObject DieMorphPrefab = null;

    [SerializeField]
    GameObject NitroParticlesPrefab = null;

    [SerializeField]
    float swimSpeed;

    [SerializeField]
    float swimFastSpeed;

    bool isSwimming;
    bool isSwimmingFast;
    bool isHurt;

    bool upwardMotion;
    bool moveForwardAfterTimer;
    float upwardMotionTimer;
    float upwardMotionDuration = 0.5F; // 500ms delay in the upward motion of the player

    public bool nitroActive;
    float nitroTimer;
    float nitroDuration = 3.0F; // 3 seconds
    bool nitroActivateTimer;

    bool dead;
    float deadTimer;
    float deadDuration = 2.0F;

    float hurtDuration = 1.5f;
    float hurtTimer;
    float limit = 6.5F; // Screen limit
    Vector2 faceDirection;
    Vector3 respawnPosition = new Vector3(0, 1.8F, 0);

    public List<GameObject> nitroTankInventory = new List<GameObject>();

    AudioSource hurtSound;
    AudioSource dieSound;
    AudioSource nitroSound;

    Animator animator;
    Rigidbody2D rigidBody2D;
    Transform spriteChild;
    LifeGenerator lifeGenerator;
    Boat boat;
    GameObject particles;

    void Start() {
        /*
         * We get the components made in the player's inspector
         */
        animator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteChild = transform.Find("PlayerSprite");
        lifeGenerator = GameObject.Find("LifeGenerator").GetComponent<LifeGenerator>();
        boat = GameObject.Find("Boat").GetComponent<Boat>();
        particles = null;

        // Audio
        AudioSource[] audioSources = GetComponents<AudioSource>();
        hurtSound = audioSources[0];
        dieSound = audioSources[1];
        nitroSound = audioSources[2];
    }

    void Update() {
        ControlCharacter();
        CheckBoundaries();

        animator.SetBool("isSwimming", isSwimming);
        animator.SetBool("isSwimmingFast", isSwimmingFast);
        animator.SetBool("isHurt", isHurt);

        HandleTimers();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Shark(Clone)" || collider.gameObject.name == "Octopus(Clone)")
        {
            if (lifeGenerator.lives.Count == 2)
            {
                lifeGenerator.RemoveLife();
                isHurt = true;
                hurtSound.Play();
            }
            else if (lifeGenerator.lives.Count == 1)
            {
                if (boat.items.Count != 0)
                {
                    Destroy(boat.items[boat.items.Count - 1]); // If any item in player inventory, destroy
                    boat.items.Clear();
                    boat.itemsCollected.Clear();
                    ResetSpeed();
                }
                lifeGenerator.RemoveLife();
                Die();
            }
        }
    }

    private void HandleTimers() {
        if (isHurt) {
            hurtTimer += Time.deltaTime;
            if (hurtTimer >= hurtDuration) {
                isHurt = false;
                hurtTimer = 0.0f;
            }
        }

        if (dead) {
            deadTimer += Time.deltaTime;
            if (deadTimer > deadDuration) {
                dead = false;
                deadTimer = 0.0f;
                transform.localScale = new Vector3(1.646864F, 1.693038F, 1.2313F); // Respawn
                transform.position = respawnPosition;
                lifeGenerator.Generate();
            }
        }

        if (upwardMotion)
            upwardMotionTimer += Time.deltaTime;
        if (upwardMotionTimer >= upwardMotionDuration)
            moveForwardAfterTimer = true;
        if (moveForwardAfterTimer) {
            float speed = swimFastSpeed;
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            SpriteDirectionUp(Vector2.up);
            //isSwimming = true;
            isSwimmingFast = true;
            upwardMotion = false;
            upwardMotionTimer = 0.0F;
        }

        if (nitroActive && Input.GetButton("Boost") && !dead) {
            nitroActive = false;
            nitroActivateTimer = true;
            if (nitroTankInventory.Count != 0)
                Destroy(nitroTankInventory[0]);
            nitroTankInventory.Clear();
            // Invicible power with particle effect
            nitroSound.Play();
            particles = Instantiate(NitroParticlesPrefab, transform.position, Quaternion.identity) as GameObject;
            particles.transform.parent = transform;
            IncreaseSpeed(3.0F);
        }
        if (nitroActivateTimer)
            nitroTimer += Time.deltaTime;
        if (nitroTimer >= nitroDuration && !dead) {
            ResetNitroTankEffect();
        }
    }

    private void ResetNitroTankEffect() {
        nitroActivateTimer = false;
        nitroTimer = 0.0f;
        Destroy(particles);
        ResetSpeed();
    }

    private void Die() {
        if (nitroActivateTimer)
            Destroy(particles);
        dieSound.Play();
        dead = true;
        transform.localScale = new Vector3(0, 0, 0); // Hide player (deleted and dead)
        GameObject morph = Instantiate(DieMorphPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(morph, deadDuration);
        // After 2 seconds, load main menu here later
    }

    private void ControlCharacter() {
        /* Speed Character */
        float speed = swimFastSpeed;

        /* Swimming Fast */
        if (Input.GetButtonDown("Run")) isSwimmingFast = true;
        if (Input.GetButtonUp("Run")) isSwimmingFast = false;

        isSwimming = false;

        if (Input.GetButtonDown("Up")) {
            upwardMotion = true;
        }
        if (Input.GetButtonUp("Up")) {
            upwardMotion = false;
            moveForwardAfterTimer = false;
            //isSwimming = false;
            isSwimmingFast = false;
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
        if (transform.position.x < -limit) {
            transform.position = new Vector2(limit, transform.position.y);
        }
        else if (transform.position.x > limit) {
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

    public void IncreaseSpeed(float number) {
        swimSpeed = swimSpeed + number;
        swimFastSpeed = swimFastSpeed + number;
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
