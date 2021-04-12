using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public List<Vector3> path;
    public float speed;

    public float slowDuration = 0;
    public float slow = 0;

    
    private void Start() {
        GameGrid.grid.scaleToGrid(transform, 0.7f);
        UpdatePath();
    }

    void Update()
    {
        float time = Time.deltaTime;
        slowDuration -= Time.deltaTime;
        if (slowDuration <= 0) {
            slow = 0;
        }
        if (path.Count == 0) return;

        Vector3 pos = transform.position;
        
        float dist = Vector2.Distance(pos, path[path.Count - 1]);
        float t = (time * (speed - speed * slow)) / dist;
        pos = Vector3.Lerp(pos, path[path.Count - 1], t);
        transform.position = pos;

        if (t >= 1 || dist < 0.0001f) {
            transform.position = path[path.Count - 1];
            path.RemoveAt(path.Count - 1);
        }
    }


    public void UpdatePath() {
        path = GameGrid.pathfinder.FindPath(transform.position);
    }

    public void Slow(float amount, float duration) {
        slowDuration = duration;
        slow = amount;
    }


}


