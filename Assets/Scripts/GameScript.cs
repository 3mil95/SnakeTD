using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public static GameScript main;

    void Start()
    {
        main = this;
    }

    public void GameOver() {
        SceneManager.LoadScene(0);
    }
}
