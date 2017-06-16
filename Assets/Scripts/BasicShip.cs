using UnityEngine;
using System.Collections;

public class BasicShip : TappableObject {

    public float speed = 5f;

    Transform target;

    public GameObject explosion;

    SpriteRenderer r;

    // Use this for initialization
	void Start () {
        r = GetComponent<SpriteRenderer>();
        target = GameManager.Instance.GetRandomBuilding();
        transform.Rotate(0,0, -1*Mathf.Atan((target.position.x - transform.position.x) / (target.position.y - transform.position.y)) * Mathf.Rad2Deg );
        PickColor();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.gameState==GameManager.GameState.running) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Mathf.Max(speed,GameManager.Instance.difficulty/3) * Time.deltaTime);
        } else if (GameManager.Instance.gameState == GameManager.GameState.ended) {
            Destroy(this.gameObject);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collide Ship");
        KillShip(r.color);
    }

    void KillShip(Color c) {
        GameObject g=(GameObject)Instantiate(GameManager.Instance.explosion, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360.0f)));
        Splosion s = g.GetComponent<Splosion>();
        s.color = c;
        s.Lifetime = GameManager.Instance.shipExplosionLife;
        Destroy(this.gameObject);
    }

    public override void OnTap() {
        Color c = r.color;
        Color b = GameManager.Instance.ActiveBuilding.color;
        c = new Color(Mathf.Max(c.r - b.r,0), Mathf.Max(c.g - b.g,0), Mathf.Max(c.b - b.b,0), 1);
        if (r.color != c) {
            GameManager.Instance.HitShip();
            if (c.r == c.g && c.g == c.b && c.b == 0) {
                KillShip(b);
            }
            r.color = c;
        }
    }

    void PickColor() {
        int numColors=Mathf.Min(4, (int)GameManager.Instance.difficulty);
        float[] newColor = new float[3];
        for ( int i =0; i<3; i++) {
            newColor[i] = 0f;
        }
        for (int i=0; i<numColors; i++) {
            newColor[Random.Range(0, 3)] = 1f;
        }
        r.color = new Color(newColor[0], newColor[1], newColor[2], 1f);
    }
}
