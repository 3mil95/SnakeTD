using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pick up type", menuName = "PickUp/PickUpType", order = 1)]
public class PickUpType : ScriptableObject
{
    public Color color;
    public GameObject tail; 
    
}
