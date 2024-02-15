using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource audioSource;
    public bool sound = true;
    [SerializeField] private Image soundImag;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if(instance != null)
           Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    public void PlaySoundFX(AudioClip clip, float voulme)
    {
        if(sound)
        {
            audioSource.PlayOneShot(clip, voulme);
        }
    }

    public void SoundOnOff()
    {
        sound = !sound;
        // if(sound)
        // {
        //     soundImag.sprite = soundon;
        // }
        // else
        //     soundImag.sprite = soundoff;
    }
}
