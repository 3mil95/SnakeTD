using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    public float speed = 1;
    private float time = 1;
    private Vector3 vel = new Vector3(1,0,0);

    bool hasTurned = false;
    bool hasCollided = false;

    Tail next;
    Tail tail;

    public GameObject tailGO;

    [SerializeField]
    private SoundPlayer soundPlayer;
    [SerializeField] 
    private GameObject startTailType;
    [SerializeField]
    private GameObject explotion;

    [SerializeField]
    private int numberOfTailsAtStart = 5;

    private SpriteRenderer spriteRenderer;

    public static int length = 1;

    


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfTailsAtStart; i++) {
            addTail(startTailType);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D other) {
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


        if (other.gameObject.tag != "Enemy") {
            GameScript.main.GameOver(); 
            DestroySnake();
            return;
        };
        
        if (next == null && !hasCollided) {
            GameScript.main.GameOver();
            DestroySnake();
            return;
        }
        
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        enemy.health.EnemyDestroy();
        RemoveTail();
    }



    private void addTail(GameObject newTail) {
        Vector3 instantiatePos = new Vector3(1000,1000,1);
        GameObject go = Instantiate(newTail, instantiatePos, Quaternion.identity);
        addTail(go.GetComponent<Tail>());
        go.transform.parent = transform.parent;
        float scale = GameGrid.grid.celSize;
        go.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void RemoveTail() {
        length--;
        if (hasCollided) return;
        hasCollided = true;
        next.Explosion();
        GameObject tailRemoved = next.gameObject;
        next = next.next;
        if (next == null) {
            next = null;
            tail = null;
        }
        Destroy(tailRemoved);
        transform.position = tailRemoved.transform.position;
    }

    private void addTail(Tail newTail) {
        length++;
        if (tail) {
            tail.AddTail(newTail);
            tail = newTail;
            return;
        }
        tail = newTail;
        next = newTail;
    }

    public void DestroySnake() {
        if (next != null) {
            next.DestroyTail();
        }
        ScreenShake.main.ScreenShack();
        ExplosionHandler.Explosion(transform.position, spriteRenderer.color);
        Destroy(gameObject);
    }

    public void Move() {
        Vector3 pos = transform.position;
        Vector3 newPos = pos + transform.up * GameGrid.grid.celSize;
        
        if (!GameGrid.grid.isInGrid(newPos)) {
            DestroySnake();
            GameScript.main.GameOver();
            return;
        }

        transform.position = newPos;

        GameGrid.grid.setStatInGrid(pos, false);
        GameGrid.grid.setStatInGrid(newPos, true);

        if (next) {
            next.Move(pos);
        }
    }

    private void handleInputs() {
        float input = 0;

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !hasTurned) {
            input = 1;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && !hasTurned) {
            input = -1;
        } else if (Input.touchCount > 0 && !hasTurned) {
            if (Input.GetTouch(0).phase == TouchPhase.Began) {
                float clickPosX = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x;
                if (clickPosX > 0) {
                    input = -1;
                } else if (clickPosX < 0) {
                    input = 1;
                }
            }
        }
        if (input != 0) {
            if (!GameScript.main.IsStarted()) { return; }
            transform.Rotate(new Vector3(0,0,90) * input);
            hasTurned = true;
        }
    }

    // Update is called once per frame
    private void Update() {
        handleInputs();
        
        time -= Time.deltaTime * GameScript.main.timeScale;
        if (time <= 0) {
            Move();
            time = speed;
            hasTurned = false;
            hasCollided = false;
            EnemyHandler.main.UpdateEnemyPaths();
        } 

    }
}
