using UnityEngine;
using System.Collections;

public class Splosion : MonoBehaviour {

    public float lifetime = .5f;

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyLater(lifetime));
	}

    IEnumerator DestroyLater(float t) {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }
}
