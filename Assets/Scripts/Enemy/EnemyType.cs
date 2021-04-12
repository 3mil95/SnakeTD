using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy type", menuName = "Enemies/Enemy type")]
public class EnemyType : ScriptableObject
{
    public Color color;
    public float moveSpeed;
    public float health;
}
