using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public float limit;
    public float speed; // Will increase after a fixed time hinting towards change of level
    public float spawnedPositionY;

    EnemySpawner enemySpawner;

    public virtual void Start() {
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
    }

    public virtual void Update() {
        transform.Translate(-Vector2.left * speed * Time.deltaTime);
    }

    public virtual float GetSpeed() {
        return speed;
    }

    public virtual void SetSpeed(float number) {
        speed = number;
    }

    public virtual void IncrementSpeed(float number) {
        speed = speed + number;
    }

    public virtual void OnDestroy() {
        enemySpawner.RemoveEnemy(gameObject);
    }
}
