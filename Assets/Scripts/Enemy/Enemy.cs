using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyMove move;
    public EnemyHealth health;
    public GameObject explotion;
    private Color color;

    public SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        EnemyHandler.main.AddEnemy(this);
        health.enemy = this;
    }

    public void setPropertys(EnemyType enemyType) {
        health.setMaxHealth(enemyType.health);
        move.speed = enemyType.moveSpeed;
        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        color = enemyType.color;
        spriteRenderer.color = enemyType.color;
    }

    public void DestroyEnemy() {
        ScreenShake.main.ScreenShack();
        ExplosionHandler.Explosion(transform.position, spriteRenderer.color);

        EnemyHandler.main.RemoveEnemy(this);
        Destroy(gameObject);
    }


}
