using UnityEngine;
using System.Collections;

public class Building : TappableObject {

    public Color color;

    Renderer r;

	// Use this for initialization
	void Start () {
        r = GetComponent<Renderer>();
        r.material.color = color;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnTap() {
        GameManager.Instance.ActiveBuilding = this;
    }
}
