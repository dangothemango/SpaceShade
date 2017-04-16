using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Atmosphere : MonoBehaviour {

    Renderer r;

    public float damagePerHit = .5f;

    public Text displayText;

    private void Start() {
        r = GetComponent<Renderer>();
    }

    private void Update() {
        Color c = r.material.color;
        if (c.a < 1f) {
            c.a += .0002f;
            r.material.color = c;
            UpdateHealthDisplay(c.a);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collide");
        Color c =r.material.color;
        c.a -= damagePerHit;
        r.material.color = c;
        UpdateHealthDisplay(c.a);
        if (c.a <= 0) {
            GameManager.Instance.GameOver();
        }
    }

    void UpdateHealthDisplay(float newValue) {
        displayText.text = string.Format("Shield: {0}%", Mathf.Min(100,Mathf.Max((int)(newValue * 100),0)));
    }
}
