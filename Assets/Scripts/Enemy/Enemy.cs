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
        GameObject go = Instantiate(explotion, transform.position - new Vector3(0,0,2), Quaternion.identity);
        ParticleSystem ps = go.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = color;
        GameGrid.grid.scaleToGrid(go.transform,0.5f);
        Destroy(go, 1);

        EnemyHandler.main.RemoveEnemy(this);
        Destroy(gameObject);
    }


}
