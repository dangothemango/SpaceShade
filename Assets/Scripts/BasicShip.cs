using UnityEngine;
using System.Collections;

public class BasicShip : MonoBehaviour {

    public float speed = 5f;

    Transform target;

    public GameObject explosion;

    // Use this for initialization
	void Start () {
        target = GameManager.Instance.GetRandomBuilding();
        transform.Rotate(0,0, -1*Mathf.Atan((target.position.x - transform.position.x) / (target.position.y - transform.position.y)) * Mathf.Rad2Deg );
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.playing) {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("Collide Ship");
        Instantiate(explosion, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360.0f)));
        Destroy(this.gameObject);
    }
}
