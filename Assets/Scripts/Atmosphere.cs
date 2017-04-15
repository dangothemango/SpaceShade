using UnityEngine;
using System.Collections;

public class Atmosphere : MonoBehaviour {

    Renderer r;

    public float damagePerHit = .5f;

    private void Start() {
        r = GetComponent<Renderer>();
    }

    private void Update() {
        Color c = r.material.color;
        if (c.a < 1f) {
            c.a += .001f;
            r.material.color = c;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collide");
        Color c =r.material.color;
        c.a -= damagePerHit;
        r.material.color = c;
        if (c.a <= 0) {
            GameManager.Instance.GameOver();
        }
    }
}
