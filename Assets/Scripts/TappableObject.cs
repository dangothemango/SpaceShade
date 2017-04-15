using UnityEngine;
using System.Collections;

public class TappableObject : MonoBehaviour {

	public virtual void OnTap() {
        throw new System.Exception("OnTap Not Implemented for: " + this.name);
    }
}
