using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource playerDeath;

    public AudioClip theme;
    public AudioClip deathSFX;
    public AudioClip enemyDeathSFX;


    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = theme;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayDeath()
    {
        playerDeath.PlayOneShot(deathSFX);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
