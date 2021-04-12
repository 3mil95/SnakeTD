using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject enemySpawner;

    [SerializeField]
    private PickUpSpawner pickUpSpawner;

    [SerializeField]
    private EnemyType[] enemyTypes;

    private float WaveNumber = 0;
    private float timeToNextWave = 0;
    public float timeBetweenWaves = 5;

    public int numOfEnemies = 30;
    public int numberOfSpewners {get; set;} = 0;



    public static WaveHandler main; 


    void Start()
    {
        main = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (numberOfSpewners == 0) {
            for (int i = 0; i < 4; i++) {
                pickUpSpawner.SpawnPickUp();
            }
            numberOfSpewners = -1;
        }
        if (numberOfSpewners > 0) return;
        timeToNextWave -= Time.deltaTime;

        if (timeToNextWave > 0) return;
        SpawnWave();
        timeToNextWave = timeBetweenWaves;

    }

    private void SpawnWave() {
        numberOfSpewners = Random.Range(1,4);

        for (int i = 0; i < numberOfSpewners; i++) {
            GameObject go = Instantiate(enemySpawner, new Vector3(1000,1000,0), Quaternion.identity);
            EnemySpwner enemySpawnerScript = go.GetComponent<EnemySpwner>();

            float delay = Random.Range(1f, 4f);
            int enemyCount = Random.Range(4,20);
            int eTypeIndex = Random.Range(0,enemyTypes.Length);

            enemySpawnerScript.setPropertys(delay, enemyCount, enemyTypes[eTypeIndex]);

            moveSpawnerToGrid(go);
        }
    }


    private void moveSpawnerToGrid(GameObject go) {
        int gridSize =  Mathf.FloorToInt(GameGrid.grid.gridSize / 2);
        GameGrid.grid.scaleToGrid(transform, 1f);

        int side = Random.Range(0,4);
        int index = Random.Range(-gridSize, gridSize+1);

        switch (side)
        {
            case 0:
                GameGrid.grid.addObjectToGrid(new Vector2Int(index, gridSize + 1), 0, go);
                break;   
            case 1:
                GameGrid.grid.addObjectToGrid(new Vector2Int(index, -(gridSize + 1)), 0, go);
                break; 
            case 2:
                GameGrid.grid.addObjectToGrid(new Vector2Int(gridSize + 1, index), 0, go);
                break; 
            case 3:
                GameGrid.grid.addObjectToGrid(new Vector2Int(-(gridSize + 1), index), 0, go);
                break; 
        }
    }
}
