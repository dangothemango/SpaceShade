using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {



    // Update is called once per frame
    void Update() {
        if (GameManager.Instance.gameState == GameManager.GameState.running) {
            Vector3 inputLoc=Vector3.zero;
            if (Input.GetMouseButtonDown(0)) {
                Debug.Log(Input.mousePosition);
                inputLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                inputLoc.z = 0;


                //foreach (Touch t in Input.touches) {
                //    if (t.phase == TouchPhase.Began) {
                //        Debug.Log(t.position);
                //        inputLoc=t.position;
                //        inputLoc.z = 0;
                //        break;
                //    }
                //}
                if (inputLoc != Vector3.zero) {
                    RaycastHit2D hit = Physics2D.Raycast(inputLoc, Vector2.zero);
                    if (hit && hit.collider != null) {
                        Debug.Log("I'm hitting " + hit.collider.name);
                        foreach (TappableObject tp in hit.transform.gameObject.GetComponents<TappableObject>()) {
                            if (tp.GetType() != typeof(Building)) {
                                DrawShot(inputLoc);
                            }
                            tp.OnTap();
                        }
                    }
                    else {
                        DrawShot(inputLoc);
                    }
                }
            }
        }
    }

    void DrawShot(Vector3 position) {
        GameObject g = (GameObject)Instantiate(GameManager.Instance.explosion, position, Quaternion.Euler(0, 0, Random.Range(0, 360.0f)));
        Splosion s = g.GetComponent<Splosion>();
        s.color = GameManager.Instance.ActiveBuilding.color;
        s.Lifetime = GameManager.Instance.shotExplosionLife;
        g.transform.localScale /= 2;
    }

    
}