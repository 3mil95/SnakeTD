using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTurret : MonoBehaviour
{
    public GameObject bomb;
    public float fierRate = 1;
    public float range = 2;
    private float time = 0;
    [SerializeField]
    private SoundPlayer soundPlayer;


    void Start()
    {
        float cellSize = GameGrid.grid.celSize;
        range = cellSize * range;      
    }

    void Update() {
        time -= Time.deltaTime;

        if (time <= 0) {
            Shoot();
        }
    
    }

    void Shoot() {
        Enemy enemy = EnemyHandler.main.GetNearestEnemy(transform.position, range);
        if (!enemy) return; 

        //soundPlayer.PlaySound();
        Debug.DrawLine(transform.position, enemy.transform.position, Color.green);
        GameObject go = Instantiate(bomb, transform.position, Quaternion.identity);
        Bomb bombObject = go.GetComponent<Bomb>();
        bombObject.SetPath(enemy.transform.position);

        time = fierRate;
    }


}
