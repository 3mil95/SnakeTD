using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public static EnemyHandler main;
    private List<Enemy> enemies;


    private void Awake()
    {
        main = this;
        enemies = new List<Enemy>();
    }

    public void AddEnemy(Enemy enemy) {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy) {
        enemies.Remove(enemy);
    }

    public void UpdateEnemyPaths() {
        foreach (Enemy enemy in enemies) {
            enemy.move.UpdatePath();
        }
    }

    public void BombEnemies(Vector3 pos, float range, float dmg, AnimationCurve curve) {
        float nearestDist = range*range;

        for (int i = enemies.Count-1; i >= 0; i--) {
            Debug.Log(i);
            Enemy enemy = enemies[i];
            float sqDist = Mathf.Pow(pos.x - enemy.transform.position.x, 2) + Mathf.Pow(pos.y - enemy.transform.position.y, 2);
            if (sqDist < nearestDist) {
                float mult = 1 - sqDist / range;
                if (mult < 0) {
                    mult = 0;
                }
                enemy.health.TakeDamage(dmg * mult);
            }
        }
    }

    public Enemy GetNearestEnemy(Vector3 pos, float range) {

        Enemy nearestEnemy = null;
        float nearestDist = range*range;

        foreach (Enemy enemy in enemies) {
            float sqDist = Mathf.Pow(pos.x - enemy.transform.position.x, 2) + Mathf.Pow(pos.y - enemy.transform.position.y, 2);
            if (sqDist < nearestDist) {
                nearestEnemy = enemy;
                nearestDist = sqDist;
            }
        }

        return nearestEnemy;
    }

}
