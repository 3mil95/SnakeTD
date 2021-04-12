using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{

    public int gridSize = 15;
    public float celSize = 1f;
    public GameObject cell;
    public GameObject nesxus;
    public GameObject head;
    public static GameGrid grid; 
    public static Pathfinder pathfinder;

    public bool[,] gridState;

    private float gridStart;
    private float gridEnd;


    private void Awake() {
        grid = this;
        gridState = new bool[gridSize,gridSize];
        pathfinder = GetComponent<Pathfinder>();

        Vector3 c = Camera.main.ScreenToWorldPoint(new Vector3(1,1,1));
        float size = Mathf.Min(Mathf.Abs(c.x), Mathf.Abs(c.y));
        Debug.Log(size);

        celSize = Mathf.Floor((size * 2) / (gridSize) * 10) / 10;
         
        gridStart = -Mathf.Floor(gridSize / 2f) * celSize;
        gridEnd = -gridStart;


        int headStart = (int) Mathf.Floor(gridSize / 4f);
        int xi =  - (int) Mathf.Floor(gridSize / 2f);
        for (float x = gridStart; x <= gridEnd; x+=celSize) {
            int yi = - (int) Mathf.Floor(gridSize / 2f) - 1;
            for (float y = gridStart; y <= gridEnd; y+=celSize) {
                yi++;
                if (yi == 0 && xi == 0) {
                    GameObject nexus = Instantiate(nesxus, new Vector3(x,y,0), Quaternion.identity);
                    nexus.transform.localScale = new Vector3(celSize, celSize, celSize);
                    nexus.transform.parent = transform;
                    continue;
                }
                if (yi == 0 && xi == headStart) {
                    head.transform.position = new Vector3(x,y,-2);
                    head.transform.localScale = new Vector3(celSize, celSize, celSize);
                }
                GameObject go = Instantiate(cell, new Vector3(x,y,0), Quaternion.identity);
                go.transform.localScale = new Vector3(celSize * 0.9f, celSize * 0.9f, celSize * 0.9f);
                go.transform.parent = transform;
                
            }
            xi++;
        }
    }

    public void addObjectToGrid(Vector2Int index, float z, GameObject theObject) {
        float x =  index.x * celSize;
        float y =  index.y * celSize;
        Vector3 pos = new Vector3(x,y,z);
        theObject.transform.position = pos;
    }

    public Vector3 GridToWorld(Vector2Int index, float z) {
        float x =  index.x * celSize;
        float y =  index.y * celSize;
        return new Vector3(x,y,z);
    }

    public Vector2Int WorldToGrid(Vector3 pos) {
        int maxIndex = Mathf.FloorToInt(gridSize / 2f);
        int xi = (Mathf.RoundToInt(pos.x / celSize));
        int yi = (Mathf.RoundToInt(pos.y / celSize));

        return new Vector2Int(xi, yi);
    }

    public void setStatInGrid(Vector3 pos, bool newState) {
        int maxIndex = Mathf.FloorToInt(gridSize / 2f);
        int xi = (Mathf.RoundToInt(pos.x / celSize) + maxIndex);
        int yi = (Mathf.RoundToInt(pos.y / celSize) + maxIndex);

        try {
            gridState[xi,yi] = newState;
        } catch {}
         
    }

    public bool isGridCellFree(Vector2Int index) {
        int maxIndex = Mathf.FloorToInt(gridSize / 2f);

        try {
            return !gridState[index.x + maxIndex,index.y + maxIndex];
        } catch {
            return false;
        }
    }


    public bool isInGrid(Vector3 pos) {
        int maxIndex = Mathf.FloorToInt(gridSize / 2f);

        int xi = Mathf.Abs(Mathf.RoundToInt(pos.x / celSize));
        int yi =  Mathf.Abs(Mathf.RoundToInt(pos.y / celSize));

        if (xi > maxIndex || yi > maxIndex) {
            return false;
        }

        return true;
    }

    public void scaleToGrid(Transform item, float scale) {
        item.localScale = new Vector3(celSize * scale,celSize * scale, celSize * scale);
    }



    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmosSelected()
    {

        if (gridState == null) return;

        int maxIndex = Mathf.FloorToInt(gridSize / 2f);

        for (int xi = -1; xi <= gridSize; xi++) {
            for (int yi = -1; yi <= gridSize; yi++) {
                if (!isGridCellFree(new Vector2Int(xi,yi))) {
                    float x = (xi - maxIndex) * celSize;
                    float y = (yi - maxIndex) * celSize;
                    Gizmos.color = Color.white;
                    Gizmos.DrawSphere(new Vector3(x,y,-1), celSize/3);
                }
            }
        }

        // Draw a yellow sphere at the transform's position
        
    }
}
