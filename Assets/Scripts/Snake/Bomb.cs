using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float dmg;
    public float range = 3;
    public AnimationCurve dmgCurve; 
    public float speed;
    private Vector3 startPos; 
    private Vector3 endPos;
    private float dist;
    [SerializeField]
    private GameObject explotion;

    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameGrid.grid.scaleToGrid(transform, 0.4f); 
        range = range * GameGrid.grid.celSize;
        startPos = transform.position;     
    }

    public void SetPath(Vector3 endPos) {
        this.endPos = endPos;
        dist = Vector3.Distance(startPos, endPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (endPos == null) return;  

        time += Time.deltaTime;

        float t = (time * speed) / dist;
        transform.position = Vector3.Lerp(startPos, endPos, t);

        if (t >= 1) {
            EnemyHandler.main.BombEnemies(transform.position, range, dmg, dmgCurve);
            GameObject expObject = Instantiate(explotion, transform.position, Quaternion.identity);
            expObject.transform.localScale = new Vector3(2 * range, 2 * range, 2 * range);
            ScreenShake.main.ScreenShack();
            Destroy(expObject, 0.1f);
            Destroy(gameObject);
        }  
    }
}
