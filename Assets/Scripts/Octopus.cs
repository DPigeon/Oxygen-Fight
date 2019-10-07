using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : Enemy {
    public override void Start() {
        limit = 5.8F;
        speed = 2.5F;
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

    public override void ChangeSpeed(float number) {
        base.ChangeSpeed(number);
    }

    public override void ResetSpeed() {
        speed = 2.5F;
    }
}
