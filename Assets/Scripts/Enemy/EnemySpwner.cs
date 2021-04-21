using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemySpwner : MonoBehaviour
{

    public int enemyCount;
    public GameObject enemy;
    private EnemyType enemyType;
    public float spawnRate;
    public float delay;

    [SerializeField]
    private TextMeshProUGUI countText;

    private SpriteRenderer spriteRenderer;


    private float time = 0;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameGrid.grid.scaleToGrid(transform, 1f);
        UpdateUI();
        setOpacity(0, true);
    }

    void setColor(Color color) {
        color.a = 0;
        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.color = color;
    }

    public void setPropertys(float delay, int enemyCount, EnemyType enemyType) {
        this.delay = delay;
        this.enemyCount = enemyCount;
        this.enemyType = enemyType;
        setColor(enemyType.color);
    }

    void setOpacity(float opacity) {
        setOpacity(opacity, false);
    }

    void setOpacity(float opacity, bool set) {
        if (!(opacity >= 0 || set)) return;
        Color c = spriteRenderer.color;
        c.a = opacity;
        spriteRenderer.color = c;
    }

    // Update is called once per frame
    void Update()
    {
        delay -= Time.deltaTime * GameScript.main.timeScale;

        if (delay > 0) {
            setOpacity((2-delay) / 2);
            return;
        }

        time -= Time.deltaTime * GameScript.main.timeScale;
        if (time <= 0) {
            GameObject go = Instantiate(enemy, transform.position, Quaternion.identity);
            Enemy enemyObject = go.GetComponent<Enemy>();
            enemyObject.setPropertys(enemyType);
            time = spawnRate;

            enemyCount--;
            UpdateUI();
            if (enemyCount <= 0) {
                WaveHandler.main.numberOfSpewners--;
                Destroy(gameObject);
            }
        }   
    }

    void UpdateUI() {
        countText.text = enemyCount.ToString();
    }
}
