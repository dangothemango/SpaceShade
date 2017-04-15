using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public bool debug = false;

    [Header("Balance Variables")]
    public float difficulty;
    //as of now increase difficulty over time, experiment with difficulty as a function of score
    //although these are probably the same...
    public float difficultyRamp = .01f;
    public bool playing = false;
    public float spawnRate = 300;

    [Header("Object Reference")]
    public Transform[] buildings;

    //Runtime Helpers
    Building activeBuilding;
    public int score = 0;

    public Building ActiveBuilding {
        get {
            return activeBuilding;
        }
        set {
            activeBuilding = value;
            Debug.Log("Setting Active Building");
        }
    }

    // Use this for initialization
    void Awake() {
        if (GameManager.Instance == null || GameManager.Instance.playing) {
            GameManager.Instance = this;
        }
    }

    void Start () {
        StartCoroutine(Countdown());
	}
	
	// Update is called once per frame
	void Update () {
        if (playing) {
            difficulty += difficultyRamp * Time.deltaTime;
        }
	}

    private void OnApplicationPause(bool pause) {
        playing = pause;
        //TODO: pause gui
    }

    IEnumerator Countdown() {
        for (int i=3; i>0; i--) {
            yield return new WaitForSeconds(1);
            //TODO: Add GUI component
        }
        playing = true;
    }

    public Transform GetRandomBuilding() {
        return buildings[Random.Range(0, buildings.Length)];
    }

    public void HitShip() {
        score += (int)(10*difficulty);
        Debug.Log(score);
    }

    public void GameOver() {
        Debug.Log("Score: " + score.ToString());
    }
}
