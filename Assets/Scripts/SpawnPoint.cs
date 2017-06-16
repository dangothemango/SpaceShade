using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

    float spawnDiff = 0f;

    public GameObject ship;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.Instance.gameState==GameManager.GameState.running) {
            spawnDiff += Time.deltaTime;
            if((Random.Range(0, (int)((GameManager.Instance.spawnRate*3)/GameManager.Instance.difficulty))==0)||spawnDiff>=GameManager.Instance.spawnRate*3/60f) {
                Instantiate(ship, transform.position, new Quaternion());
                spawnDiff = 0;
            }
        }
	}
}
