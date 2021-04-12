using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostTurret : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimationCurve curve;
    public float fierRate = 1;
    public float fierTime = 2;
    public float range = 2;


    public float Damage;
    public float slowAmount = 0.4f;
    public float slowDuration = 2f;

    private float time = 0;

    private Vector3 startScale;
    private Vector3 endScale;
    


    void Start()
    {
        GameGrid.grid.scaleToGrid(transform, 1f);
        startScale = transform.localScale;
        endScale = startScale + new Vector3(range, range, 0) * GameGrid.grid.celSize * 2; 
    }

    private void Update() {
        time += Time.deltaTime;
        float cTime = time;

        if (cTime < fierRate) return;
        cTime -= fierRate;
        float t = curve.Evaluate(cTime / fierTime);
        Vector3 newScale = Vector3.Lerp(startScale, endScale, t);
        transform.localScale = newScale;

        if (cTime < fierTime) return;
        time = 0;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag != "Enemy") return;
        Enemy enemy = other.GetComponent<Enemy>();
        enemy.health.TakeDamage(Damage);
        enemy.move.Slow(slowAmount, slowDuration);
    }

}
