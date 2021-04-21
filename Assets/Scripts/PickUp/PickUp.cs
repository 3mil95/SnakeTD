using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private SpriteRenderer sprite;
    public Color color;
    public GameObject tail; 

    public float lifeTime = 10f;



    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        GameGrid.grid.scaleToGrid(transform, 0.8f);
    }

    private void Update() {
        lifeTime -= Time.deltaTime * GameScript.main.timeScale;
        setOpacity(lifeTime / 5);
        if (lifeTime <= 0) {
            Destroy(gameObject);
        }
    }

    void setOpacity(float opacity) {
        if (opacity > 1) return;
        Color c = sprite.color;
        c.a = opacity;
        sprite.color = c;
    }

    public void setPickUpType(PickUpType pickUpType) {
        if (sprite == null) {
            sprite = GetComponent<SpriteRenderer>();
        }

        tail = pickUpType.tail;
        color = pickUpType.color;

        sprite.color = color;
    }
}
