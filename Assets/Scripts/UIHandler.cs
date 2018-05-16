using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

    [Header("Menu Sections")]
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject ingameOverlay;
    public GameObject pauseMenu;
    public GameObject endGameMenu;

    [Header("Ingame References")]
    public Text scoreText;
    public Text countdownText;
    public Text shieldHealthText;

    [Header("EndGame References")]
    public Text gameOverText;

    //[Header("Main References")]

    //[Header("Options References")]

    //[Header("Pause References")]

    enum Menu {
        NONE,
        MAIN,
        OPTIONS,
        INGAME,
        PAUSE,
        ENDGAME
    }

    Menu activeMenu = Menu.NONE;
    Color[] countdownColors = { Color.red, Color.blue, Color.green };

    void ShowOne(bool main = false, bool options = false, bool ingame = false, bool pause = false, bool endgame = false ) {  
        mainMenu.SetActive(main);
        optionsMenu.SetActive(options);
        ingameOverlay.SetActive(ingame);
        pauseMenu.SetActive(pause);
        endGameMenu.SetActive(endgame);
    }

    public void ShowMainMenu() {
        ShowOne(main: true);
        activeMenu = Menu.MAIN;
    }

    public void ShowPauseMenu() {
        ShowOne(pause: true);
        activeMenu = Menu.PAUSE;
    }

    public void ShowOptionsMenu() {
        ShowOne(options: true);
        activeMenu = Menu.OPTIONS;
    }

    public void ShowEndGameMenu(int score, int high) {
        ShowOne(endgame: true);
        activeMenu = Menu.ENDGAME;
        gameOverText.text = string.Format("Game Over\nScore: {0}\nHigh Score: {1}", score, high);
        gameOverText.gameObject.SetActive(true);
    }

    public void ShowIngameOverlay() {
        ShowOne(ingame: true);
        activeMenu = Menu.INGAME;
    }

    public Coroutine ShowCountdown(int n) {
        if(activeMenu != Menu.INGAME) {
            Debug.LogWarning("Ingame Menu is not active, Activate explicitly with ShowIngameOverlay()");
            ShowIngameOverlay();
        }
        return StartCoroutine(CountdownFade(n));
    }

    IEnumerator CountdownFade(int n) {
        countdownText.text = n.ToString();
        countdownText.enabled = true;
        Color c = countdownColors[n % 3];
        float i = 1;
        while (i >= 0) {
            c.a = i;
            countdownText.color = c;
            yield return null;
            i -= Time.deltaTime;
        }
        countdownText.enabled = false;
    }

    public void UpdateScoreDisplay(int score) {
        if(activeMenu != Menu.INGAME) {
            Debug.LogWarning("Ingame Menu is not active, Activate explicitly with ShowIngameOverlay()");
            ShowIngameOverlay();
        }
        scoreText.text = string.Format("Score: {0}", score);
    }

    public void UpdateShieldHealth(float health) {
        shieldHealthText.text = string.Format("Shield: {0}%", health);
    }

}
