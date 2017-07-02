using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public bool debug = false;

    [Header("Balance Variables")]
    public float difficulty = 1;
    public int countdownTime = 3;
    //as of now increase difficulty over time, experiment with difficulty as a function of score
    //although these are probably the same...
    public float difficultyRamp = .01f;
    public float spawnRate = 300;

    [Header("Object Reference")]
    public Transform[] buildings;
    public Atmosphere atmosphere;
    public Text scoreDisplay;
    public Text gameOverText;
    public SelectedBuilding selectedBuilding;
    public GameObject explosion;
    public Text countdownText;

    [Header("Asthetic Variables")]
    public float shotExplosionLife = .2f;
    public float shipExplosionLife = .5f;
    private Color[] countdownColors = { Color.red, Color.blue, Color.green };

    //Runtime Helpers
    Building activeBuilding;
    int score = 0;
    public GameState gameState = GameState.preStart;

    public enum GameState {
        preStart,
        running,
        paused,
        ended
    }

    public bool IsRunning {
        get {
            return gameState == GameState.running;
        }
    }

    public Building ActiveBuilding {
        get {
            return activeBuilding;
        }
        set {
            activeBuilding = value;
            selectedBuilding.trans = activeBuilding.transform;
            selectedBuilding.color = activeBuilding.color;
            Debug.Log("Setting Active Building");
        }
    }

    // Use this for initialization
    void Awake() {
        if (GameManager.Instance == null || GameManager.Instance.gameState == GameState.ended) {
            GameManager.Instance = this;
        }
    }

    void Start() {
        StartCoroutine(Countdown());
        ActiveBuilding = buildings[0].GetComponent<Building>();
    }

    // Update is called once per frame
    void Update() {
        if (IsRunning) {
            difficulty += difficultyRamp * Time.deltaTime;
        }
    }

    private void OnApplicationPause(bool pause) {
        if (pause) {
            gameState = GameState.paused;
            StopAllCoroutines();
        }
        else {
            Countdown();
        }
        //TODO: pause gui
    }

    IEnumerator Countdown() {
        yield return new WaitForSeconds(.5f);
        for (int i=countdownTime; i>0; i--) {
            yield return StartCoroutine(CountdownFade(i));
            //TODO: Add GUI component
        }
        gameState = GameState.running;
    }

    IEnumerator CountdownFade(int n) {
        countdownText.text = n.ToString();
        countdownText.enabled = true;
        Color c = countdownColors[n%3];
        float i = 1;
        while (i >= 0) {
            c.a = i;
            countdownText.color = c;
            yield return null;
            i -= Time.deltaTime;
        }
        countdownText.enabled = false;
    }
    
    public Transform GetRandomBuilding() {
        return buildings[Random.Range(0, buildings.Length)];
    }

    public void HitShip() {
        score += (int)(10 * difficulty);
        UpdateScoreDisplay();
    }

    public void GameOver() {
        gameState = GameState.ended;
        int high = PlayerPrefs.GetInt("HighScore", 0);
        if (score > high) {
            PlayerPrefs.SetInt("HighScore", score);
        }
        gameOverText.text = string.Format("Game Over\nScore: {0}\nHigh Score: {1}",score,high);
        gameOverText.gameObject.SetActive(true);
    }

    public void Restart() {
        gameState = GameState.preStart;
        atmosphere.SetHealth(1f);
        score = 0;
        difficulty = 1;
        gameOverText.gameObject.SetActive(false);
        UpdateScoreDisplay();
        Start();
    }

    void UpdateScoreDisplay() {
        scoreDisplay.text = string.Format("Score: {0}", score);
    } 
}
