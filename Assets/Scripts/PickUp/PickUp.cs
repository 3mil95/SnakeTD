using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private SpriteRenderer[] sprites;
    public Color color;
    public GameObject tail; 



    // Start is called before the first frame update
    void Start()
    {
        sprites = GetComponentsInChildren<SpriteRenderer>();
        GameGrid.grid.scaleToGrid(transform, 0.8f);
    }

    public void setPickUpType(PickUpType pickUpType) {
        if (sprites == null) {
            sprites = GetComponentsInChildren<SpriteRenderer>();
        }

        tail = pickUpType.tail;
        color = pickUpType.color;

        foreach (SpriteRenderer sprite in sprites) {
            sprite.color = color;
        }

    }
}
