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
    public SelectedBuilding selectedBuilding;
    public GameObject explosion;
    public UIHandler ui;

    [Header("Asthetic Variables")]
    public float shotExplosionLife = .2f;
    public float shipExplosionLife = .5f;

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
        ui.ShowMainMenu();
        ActiveBuilding = buildings[1].GetComponent<Building>();
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
            ui.ShowPauseMenu();
        }
        else {
        }
    }

    IEnumerator Countdown() {
        yield return new WaitForSeconds(.5f);
        for (int i=countdownTime; i>0; i--) {
            yield return ui.ShowCountdown(i);
        }
        gameState = GameState.running;
    }
    
    public Transform GetRandomBuilding() {
        return buildings[Random.Range(0, buildings.Length)];
    }

    public void HitShip() {
        score += (int)(10 * difficulty);
        ui.UpdateScoreDisplay(score);
    }

    public void GameOver() {
        gameState = GameState.ended;
        int high = PlayerPrefs.GetInt("HighScore", 0);
        if (score > high) {
            PlayerPrefs.SetInt("HighScore", score);
        }
        ui.ShowEndGameMenu(score, high);
    }

    public void StartGame() {
        gameState = GameState.preStart;
        atmosphere.SetHealth(1f);
        score = 0;
        difficulty = 1;
        ui.ShowIngameOverlay();
        ui.UpdateScoreDisplay(score);
        StartCoroutine(Countdown());
    }
}
