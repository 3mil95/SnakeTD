using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundPlayer : MonoBehaviour
{

    private float pitch;
    public float pitchVariation = 0;
    [SerializeField]
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        setRandomPitch(); 
    }


    void setRandomPitch() {
        pitch = audioSource.pitch;
        float newPitch = pitch + Random.Range(-pitchVariation, pitchVariation);
        audioSource.pitch = newPitch;
    }

    public void PlaySound() {
        setRandomPitch();
        audioSource.Play();        
    }
}
