using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 1;
    private float health;


    [SerializeField]
    private Slider healthBar;

    public Enemy enemy { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void setMaxHealth(float maxHealth) {
        this.maxHealth = maxHealth;
        health = maxHealth;
    }

    private void OnCollisionEnter(Collision other) {
       EnemyDestroy();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != "Nexus") return;
        EnemyDestroy();
    }

    public void EnemyDestroy() {
        if (!enemy) return;
        enemy.DestroyEnemy();
    }

    public void TakeDamage(float dmg) {
        health -= dmg;
        healthBar.value = health / maxHealth;
        if (health <= 0) {
            EnemyDestroy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
