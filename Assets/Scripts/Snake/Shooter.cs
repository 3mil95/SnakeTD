using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{

    public float dmg = 1;
    public float speed = 1;
    public float range = 2;

    private float time = 0;

    [SerializeField]
    private SoundPlayer soundPlayer;

    [SerializeField]
    private LineRenderer beam;

    private void Start() {
        float cellSize = GameGrid.grid.celSize;
        beam.startWidth = cellSize * 0.1f;
        beam.endWidth = cellSize * 0.1f;
        range = cellSize * range;
    }

    void Update() {
        time -= Time.deltaTime * GameScript.main.timeScale;

        if (time <= speed * 0.95f) {
            beam.positionCount = 0;
        }

        if (time <= 0) {
            Shoot();
        }
    
    }

    void DrawShootRay(Vector3 from, Vector3 to) {
        beam.positionCount = 2;
        beam.SetPosition(0, from);
        beam.SetPosition(1, to);
    }

    void Shoot() {
        Enemy enemy = EnemyHandler.main.GetNearestEnemy(transform.position, range);
        if (!enemy) return; 

        soundPlayer.PlaySound();
        Debug.DrawLine(transform.position, enemy.transform.position, Color.green);
        DrawShootRay(transform.position, enemy.transform.position);
        enemy.health.TakeDamage(dmg);
        time = speed;
    }
}
