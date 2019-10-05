using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour {
    float limit = 11.0F;
    float speed; // Will increase after a fixed time hinting towards change of level
    float spawnedPositionY;

    void Start() {
        speed = Random.Range(1.0F, 5.0F);
        spawnedPositionY = transform.position.y;
    }

    void Update() {
        transform.Translate(-Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < -limit) {
            transform.position = new Vector2(limit, spawnedPositionY);
        }
        else if (transform.position.x > limit) {
            transform.position = new Vector2(-limit, spawnedPositionY);
        }
    }
}
