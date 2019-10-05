using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octopus : MonoBehaviour
{
    float limit = 5.8F;
    float speed = 3.0F; // Will increase after a fixed time hinting towards change of level

    void Start() {
    }

    void Update() {
        transform.Translate(-Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < -limit )
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
        }
        else if (transform.position.x > limit)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back);
        }
    }
}
