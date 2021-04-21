using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Nexus : MonoBehaviour
{
    public int health = 50;
    public TextMeshProUGUI healthText;
    public Head head;

    private void Start() {
        SetHealthText();
        head = FindObjectOfType<Head>();
    }

    private void SetHealthText() {
        healthText.text = health.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Enemy" && other.tag != "Player") return;

        health--;
        SetHealthText();
        
        if(health <= 0) {
            if (head != null) {
                head.DestroySnake();
            }
            GameScript.main.GameOver();
        }        
    }
}
