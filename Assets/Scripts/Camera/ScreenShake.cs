using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    // Start is called before the first frame update
    private float time = -1;
    private Vector3 pos;
    private Vector3 dir;
    public static ScreenShake main;

    public float duration = 1;
    public float dist = 2;
    
    
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position; 
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        float deltaTime = Time.deltaTime * GameScript.main.timeScale;
        if (time != -1) {
            time += deltaTime;
            transform.position = pos + dir * Mathf.Sin((time / duration) * 2 * Mathf.PI);
        }
        if (time / duration >= 1) {
            time = -1;
        }
    }


    public void ScreenShack() {
        this.dir = new Vector3(0,GameGrid.grid.celSize,0)  * dist;
        time = 0;
    }
}
