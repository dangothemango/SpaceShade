using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Atmosphere : MonoBehaviour {

    Renderer r;

    public float damagePerHit = .5f;

    private void Start() {
        r = GetComponent<Renderer>();
    }

    private void Update() {
        Color c = r.material.color;
        if (c.a < 1f && GameManager.Instance.IsRunning) {
            c.a += .0002f;
            r.material.color = c;
            UpdateHealthDisplay(c.a);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Color c =r.material.color;
        c.a -= damagePerHit;
        r.material.color = c;
        UpdateHealthDisplay(c.a);
        if (c.a <= 0) {
            GameManager.Instance.GameOver();
        }
    }

    public void SetHealth(float f) {
        Color c = r.material.color;
        c.a = f;
        r.material.color = c;
        UpdateHealthDisplay(f);
    }

    void UpdateHealthDisplay(float newValue) {
        GameManager.Instance.ui.UpdateShieldHealth(Mathf.Min(100, Mathf.Max((int)(newValue * 100), 0)));
    }
}
