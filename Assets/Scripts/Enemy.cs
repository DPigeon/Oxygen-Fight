using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float limit;
    public float speed; // Will increase after a fixed time hinting towards change of level
    public float spawnedPositionY;

    public virtual void Start() {
        
    }

    public virtual void Update() {
        transform.Translate(-Vector2.left * speed * Time.deltaTime);
    }

    public virtual void ChangeSpeed(float number) {
        speed = number;
    }

    public virtual void ResetSpeed() {
    }
}
