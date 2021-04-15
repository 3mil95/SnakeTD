using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    public Tail next { get; private set; }

    public void Move(Vector3 newPos) {
        Vector3 pos = transform.position;
        transform.position = newPos;

        GameGrid.grid.setStatInGrid(pos, false);
        GameGrid.grid.setStatInGrid(newPos, true);

        if (next) {
            next.Move(pos);
        }
    }


    public void AddTail(Tail next) {
        this.next = next;
    }
}
