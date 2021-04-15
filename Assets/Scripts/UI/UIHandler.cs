using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI waveNumberText;
    [SerializeField]
    private TextMeshProUGUI nextWaveText;

    public static UIHandler main { get; private set; }

    private void Awake() {
        main = this;
    }

    public void SetWaveNumber(int waveNumber) {
        waveNumberText.text = waveNumber.ToString();
    } 

    public void SetNextWave(float time) {
        if (time <= 0) {
            nextWaveText.enabled = false;
            return;
        }
        nextWaveText.enabled = true;
        time = Mathf.Round(time);
        nextWaveText.text = "Next Wave: " + time.ToString(); 
    }
}
