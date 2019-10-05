using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    float limit = 11.0F;
    float speed = 2.0F; // Will increase after a fixed time hinting towards change of level

    void Update()
    {
        transform.Translate(-Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < -limit || transform.position.x > limit)
            Destroy(gameObject);
    }
}
