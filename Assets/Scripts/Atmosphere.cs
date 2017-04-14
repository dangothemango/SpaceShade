using UnityEngine;
using System.Collections;

public class Atmosphere : MonoBehaviour {

    Renderer r;

    public float damagePerHit = .5f;

    private void Start() {
        r = GetComponent<Renderer>();
    }
    
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collide");
        Color c =r.material.color;
        c.a -= damagePerHit;
        r.material.color = c;
    }
}
