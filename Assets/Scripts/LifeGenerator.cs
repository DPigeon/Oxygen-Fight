using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeGenerator : MonoBehaviour {
    [SerializeField]
    GameObject Life = null;

    public List<GameObject> lives = new List<GameObject>();

    void Start() {
        Generate();
    }

    void Update() {
    }

    public void Generate() {
        Vector2 position1 = new Vector2(transform.position.x, transform.position.y);
        Vector2 position2 = new Vector2(transform.position.x + 0.6F, transform.position.y);
        GameObject life1 = Instantiate(Life, position1, Quaternion.identity) as GameObject;
        GameObject life2 = Instantiate(Life, position2, Quaternion.identity) as GameObject;
        lives.Add(life1);
        lives.Add(life2);
    }

    public void GenerateLivesAfterLevelUp() {
        // We remove twice and add new lives.
        RemoveLife();
        RemoveLife();
        Generate();
    }

    public void RemoveLife() {
        if (lives.Count != 0) {
            Destroy(lives[lives.Count - 1]);
            lives.RemoveAt(lives.Count - 1);
        }
    }
}
