using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : Enemy {
    public override void Start() {
        base.Start();
        limit = 7.0F;
        speed = 1.8F;
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

    public override float GetSpeed() {
        return speed;
    }

    public override void ChangeSpeed(float number) {
        base.ChangeSpeed(number);
    }

    public override void IncrementSpeed(float speed) {
        base.IncrementSpeed(speed);
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }
}
