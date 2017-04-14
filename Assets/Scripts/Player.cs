using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        foreach (Touch t in Input.touches) {
            if (t.phase == TouchPhase.Began) {
                Debug.Log(t.position);

            }
        }
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log(Input.mousePosition);

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero) ;
            if (hit && hit.collider != null) {
                Debug.Log("I'm hitting " + hit.collider.name);
            }
#endif
        }


    }
}