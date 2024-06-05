using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    float sfxVolume;

    private void Start()
    {
        sfxVolume = SettingsManager.sfxVol;
        audioSource.volume = SettingsManager.musicVol;
    }

    public void PlaySound(AudioClip clip, Vector3 pos) 
    { 
        AudioSource.PlayClipAtPoint(clip, pos, sfxVolume);
    }   
}
