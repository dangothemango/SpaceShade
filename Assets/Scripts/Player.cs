using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(LineFactory))]
public class Player : MonoBehaviour {

    LineFactory lineFactory;

    private void Start() {
        lineFactory = GetComponent<LineFactory>();
    }

    // Update is called once per frame
    void Update() {
        if (GameManager.Instance.gameState <= GameManager.GameState.running) {
            Vector3 inputLoc=Vector3.zero;
            if (Input.GetMouseButtonDown(0)) {
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

        //Draw Laser
        Line l = lineFactory.GetLine(GameManager.Instance.ActiveBuilding.transform.position, position, .05f, GameManager.Instance.ActiveBuilding.color);
    }

    
}