using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //This values are used for when the camera directly follows the player (including vertically), used during testing
    /* 
    private Vector3 offset = new Vector3(5f, 6f, -10f);
    private float smoothTime = .25f;
    private Vector3 velocity = Vector3.zero; */

    //Holds the player and their current X position
    [SerializeField] private Transform player;
    [SerializeField] private Transform endLevelPoint;
    private float playerXPosition;
    private bool cameraIsLocked;
    private float halfCameraWidth;

    // Start is called before the first frame update
    void Start()
    {
        halfCameraWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        cameraIsLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        //This was used for when the camera directly follows the player (including vertically), used during testing
        //Vector3 targetPosition = target.position + offset;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if (!cameraIsLocked)
        {
            //This allows the camera to only follow the player horizontally, and won't follow the player vertically
            if (player.position.x > playerXPosition)
            {
                playerXPosition = player.position.x;
            }
            transform.position = new Vector3(playerXPosition, transform.position.y, transform.position.z);

            // Lock the camera when the player reaches the end of the level
            cameraIsLocked = halfCameraWidth >= ((endLevelPoint.position.x + 2) - playerXPosition);
        }
    }
}
