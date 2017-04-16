using UnityEngine;
using System.Collections;

public class Splosion : MonoBehaviour {

    SpriteRenderer r;

    public Color color {
        get {
            return r.color;
        }
        set {
            r.color = value;
        }
    }

    public float Lifetime {
        set {
            StartCoroutine(DestroyLater(value));
        }
    }

	// Use this for initialization
	void Awake () {
        r = GetComponent<SpriteRenderer>();
	}

    IEnumerator DestroyLater(float t) {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }
}
