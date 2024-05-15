using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraYDown : MonoBehaviour
{
    //Holds the player and their current X position
    [SerializeField] private Transform player;
    [SerializeField] private Transform endLevelPoint;
    private float playerXPosition;
    private float playerYPosition;
    private bool cameraIsLocked;
    private float halfCameraWidth;

    void OnEnable()
    {
        playerYPosition = player.position.y + 6;
        transform.position = new Vector3(playerXPosition, playerYPosition, transform.position.z);
        halfCameraWidth = Camera.main.orthographicSize * Screen.width / Screen.height; // Calculate the half-width of the camera
        cameraIsLocked = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!cameraIsLocked)
        {
            //This allows the camera to only follow the player horizontally, and will follow only down vertically
            if (player.position.x > playerXPosition)
            {
                playerXPosition = player.position.x;
            }
            transform.position = new Vector3(playerXPosition, transform.position.y, transform.position.z);

            if (player.position.y < playerYPosition)
            {
                playerYPosition = player.position.y;
                transform.position = new Vector3(transform.position.x, playerYPosition + 6, transform.position.z);
            }
        // Lock the camera when the player reaches the end of the level
            cameraIsLocked = halfCameraWidth >= ((endLevelPoint.position.x + 2) - playerXPosition);
        }
    }
}