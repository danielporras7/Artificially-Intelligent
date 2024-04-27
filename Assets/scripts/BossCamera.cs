using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Different Camera script used for the final level
//The main difference is that the camera will lock once the player reaches the boss
//Afterwards the camera will unlock like normal and the player can continue to the goal

public class BossCamera : MonoBehaviour
{
    //Holds the player and their current X position
    [SerializeField] private Transform player;
    [SerializeField] private Transform endLevelPoint;
    [SerializeField] private Transform bossPoint;
    [SerializeField] private GameObject bossWall;
    [SerializeField] private GameObject bossWall2;
    [SerializeField] private GameObject boss;
    [SerializeField] private Transform bossSpawnPoint;
    [SerializeField] private Transform bossSpawnWall;

    private float playerXPosition;
    [SerializeField] private bool cameraIsLocked;
    private float halfCameraWidth;
    [SerializeField] private bool isBossDead;
    private bool lockOnce;
    private bool isBossSpawned;

    //Sounds
    SoundManager soundManager;


    // Start is called before the first frame update
    void Start()
    {
        halfCameraWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        cameraIsLocked = false;
        isBossDead = false;
        isBossSpawned = false;
        lockOnce = false;
    }

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Camera before player reaches boss
        if (!cameraIsLocked && !isBossDead)
        {
            //cameraIsLocked = false;

            //This allows the camera to only follow the player horizontally, and won't follow the player vertically
            if (player.position.x > playerXPosition)
            {
                playerXPosition = player.position.x;
            }

            transform.position = new Vector3(playerXPosition, transform.position.y, transform.position.z);

            // Lock the camera when the player reaches the Boss Fight
            cameraIsLocked = halfCameraWidth >= ((bossPoint.position.x + 2) - playerXPosition);


            // Lock the camera when the player reaches the end of the level
            //cameraIsLocked = halfCameraWidth >= ((endLevelPoint.position.x + 2) - playerXPosition);
        }

        //Boss is spawned and also a wall to lock the player in
        else if(cameraIsLocked && !isBossDead)
        {
            if(!isBossSpawned)
            {
                soundManager.StartBossTheme();
                Instantiate(boss, bossSpawnPoint.position, bossSpawnPoint.rotation);
                Instantiate(bossWall2, bossSpawnWall.position, bossSpawnWall.rotation);
                isBossSpawned = true;
            }
        }

        //Unlocks camera after boss is defeated
        else if(cameraIsLocked && isBossDead && !lockOnce)
        {
            Destroy(bossWall);
            Destroy(bossWall2);
            cameraIsLocked = false;
            soundManager.EndBossTheme();
        }

        //Camera after player defeats boss
        else if (!cameraIsLocked && isBossDead)
        {
            lockOnce = true;

            //This allows the camera to only follow the player horizontally, and won't follow the player vertically
            if (player.position.x > playerXPosition)
            {
                playerXPosition = player.position.x;
            }

            transform.position = new Vector3(playerXPosition, transform.position.y, transform.position.z);

            // Lock the camera when the player reaches the end of the level
            cameraIsLocked = halfCameraWidth >= ((endLevelPoint.position.x + 2) - playerXPosition);
        }

        //Camera is locked once the end point is reached
        else if(cameraIsLocked && isBossDead)
        {
            cameraIsLocked = true;
        }

    }

    public void BossDeath()
    {
        isBossDead = true;
    }
}
