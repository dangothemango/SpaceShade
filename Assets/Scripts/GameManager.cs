using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public bool debug = false;

    [Header("Helper Variables")]
    public float difficulty;
    //as of now increase difficulty over time, experiment with difficulty as a function of score
    //although these are probably the same...
    public float difficultyRamp = .01f;
    public bool playing = false;
    public float spawnRate = 300;


    // Use this for initialization
    void Awake() {
        if (GameManager.Instance == null || GameManager.Instance.playing) {
            GameManager.Instance = this;
        }
    }

    void Start () {
        //StartCoroutine(Countdown());
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

}
