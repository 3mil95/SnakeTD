using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject explotion;
    static private GameObject _explotion;

    static private Transform _transform;

    private void Awake() {
        _explotion = explotion;
        _transform = transform;
    }

    public static void Explosion(Vector3 position, Color color) {
        GameObject go = Instantiate(_explotion, position - new Vector3(0,0,2), Quaternion.identity);
        go.transform.parent = _transform;
        ParticleSystem ps = go.GetComponent<ParticleSystem>();
        var main = ps.main;
        main.startColor = color;
        GameGrid.grid.scaleToGrid(go.transform,0.5f);
        Destroy(go, 1);
    }
}
