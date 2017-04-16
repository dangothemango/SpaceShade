using UnityEngine;
using System.Collections;

public class SelectedBuilding : MonoBehaviour {

    SpriteRenderer r;

    public Color color {
        get {
            return r.color;
        }
        set {
            r.color = value;
        }
    }

    public Transform trans {
        get {
            return transform;
        }
        set {
            transform.position = value.position;
            transform.rotation = value.rotation;
        }
    }

	// Use this for initialization
	void Start () {
        r = GetComponent<SpriteRenderer>();
	}

}
