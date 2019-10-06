using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Enemy {

    public override void Start() {
        limit = 7.0F;
        speed = Random.Range(1.0F, 5.0F);
        spawnedPositionY = transform.position.y;
    }

    public override void Update() {
        base.Update();
        if (transform.position.x < -limit) {
            transform.position = new Vector2(limit, spawnedPositionY);
        }
        else if (transform.position.x > limit) {
            transform.position = new Vector2(-limit, spawnedPositionY);
        }
    }

    public override void IncrementSpeed(float number) {
        Debug.Log("increment");
    }

    public override void ResetSpeed() {
        speed = Random.Range(1.0F, 5.0F);
    }
}
