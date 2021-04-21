using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public int score = 0;
    private GameState gameState = GameState.Start;
    public static GameScript main;
    public float timeScale { get; set; } = 0;



    void Start()
    {
        timeScale = 0;
        main = this;
    }


    public bool IsStarted() {
        if (gameState == GameState.Started) {
            return true;
        }
        UIHandler.main.RemoveStartText();
        gameState = GameState.Started;
        timeScale = 1f;
        return false;
    }

    private void Update() {
        if (gameState == GameState.GameOver) {
            timeScale -= Time.deltaTime / 2;
            if (timeScale < 0) {
                timeScale = 0;
            }
            if ((Input.touchCount > 0 || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)) && timeScale == 0) {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void GameOver() {
        gameState = GameState.GameOver;
    }

    public void AddScore(int value) {
        score += value;
        UIHandler.main.SetScoreText(score);
    }
}

enum GameState {
    Start,
    Started,
    GameOver
}
