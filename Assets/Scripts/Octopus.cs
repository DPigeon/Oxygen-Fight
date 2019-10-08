using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : Enemy {
    public override void Start() {
        base.Start();
        limit = 5.8F;
        speed = enemySpawner.currentSpeedGameLevelOctopus;
    }

    public override void Update() {
        base.Update();
        if (transform.position.x < -limit )
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
        else if (transform.position.x > limit)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back);
        }
    }

    public override float GetSpeed() {
        return speed;
    }

    public override void SetSpeed(float number) {
        base.SetSpeed(number);
    }

    public override void IncrementSpeed(float speed) {
        base.IncrementSpeed(speed);
    }

    public override void OnDestroy() {
        base.OnDestroy();
    }
}
