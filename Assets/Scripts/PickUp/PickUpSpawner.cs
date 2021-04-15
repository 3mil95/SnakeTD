using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public GameObject pickUp;

    public PickUpType[] pickUpTypes;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnPickUps() {
        float numberOfTurrets = Random.Range(0.1f,2.1f);
        for (int i = 0; i < 4; i++) {
            bool isTurret = false;
            if (i < numberOfTurrets) {
                isTurret = true;
            }
            SpawnPickUp(isTurret);
        }
    }

    public void SpawnPickUp(bool isTurret) {
        GameObject go = Instantiate(pickUp, new Vector3(1000, -10000, -1), Quaternion.identity);
        
        PickUp pu = go.GetComponent<PickUp>();

        int type = 0;
        if (isTurret) {
            type = Random.Range(1, pickUpTypes.Length);
        }
        pu.setPickUpType(pickUpTypes[type]);

        int size = (int) Mathf.Floor(GameGrid.grid.gridSize / 2f);


        Vector2Int index = new Vector2Int(0,0);
        while(index.x == 0 && index.y == 0) {
            index.x = Random.Range(-size, size+1);
            index.y = Random.Range(-size, size+1);
        }
        
        GameGrid.grid.addObjectToGrid(index, -2, go);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnPickUp(false);
        }
    }
}
