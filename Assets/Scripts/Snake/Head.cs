using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public float speed = 1;
    private float time = 1;
    private Vector3 vel = new Vector3(1,0,0);

    bool hasTurned = false;

    Tail next;
    Tail tail;

    public GameObject tailGO;

    [SerializeField]
    private SoundPlayer soundPlayer;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("pickup");
        PickUp pu = other.gameObject.GetComponent<PickUp>();
        if (pu == null) {
            return;
        }
        GameObject tail = pu.tail;
        if (tail == null) {
            Destroy(other.gameObject);
            return;
        }
        soundPlayer.PlaySound();
        addTail(tail);
        Destroy(other.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        GameScript.main.GameOver();
    }



    private void addTail(GameObject newTail) {
        Vector3 instantiatePos = new Vector3(1000,1000,1);
        GameObject go = Instantiate(newTail, instantiatePos, Quaternion.identity);
        addTail(go.GetComponent<Tail>());
        go.transform.parent = transform.parent;
        float scale = GameGrid.grid.celSize;
        go.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void addTail(Tail newTail) {
        if (tail) {
            tail.AddTail(newTail);
            tail = newTail;
            return;
        }
        tail = newTail;
        next = newTail;
    }

    public void Move() {
        Vector3 pos = transform.position;
        Vector3 newPos = pos + transform.up * GameGrid.grid.celSize;
        transform.position = newPos;

        GameGrid.grid.setStatInGrid(pos, false);
        GameGrid.grid.setStatInGrid(newPos, true);

        if (next) {
            next.Move(pos);
        }
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !hasTurned) {
            transform.Rotate(new Vector3(0,0,90));
            hasTurned = true;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && !hasTurned) {
            transform.Rotate(new Vector3(0,0,-90));
            hasTurned = true;
        }
        
        time -= Time.deltaTime;
        if (time <= 0) {
            Move();
            time = speed;
            hasTurned = false;
            EnemyHandler.main.UpdateEnemyPaths();
        } 

        if (!GameGrid.grid.isInGrid(transform.position)) {
            GameScript.main.GameOver();
        }
    }
}
