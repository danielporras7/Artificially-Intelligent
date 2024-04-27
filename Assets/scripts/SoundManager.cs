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
    public AudioClip bossTheme;
    public AudioClip winTheme;


    // Start is called before the first frame update
    void Start()
    {
        //Comment the following code to turn off the background music
        musicSource.clip = theme;
        musicSource.Play();
    }

    //Comment the bodies of any of the methods to turn off a specific sound
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayDeath()
    {
        playerDeath.PlayOneShot(deathSFX);
    }

    public void StartBossTheme()
    {
        musicSource.Stop();
        musicSource.clip = bossTheme;
        musicSource.Play();
    }

    public void EndBossTheme()
    {
        musicSource.Stop();
        musicSource.clip = winTheme;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
