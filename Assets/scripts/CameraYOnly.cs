using UnityEngine;

public class CameraYOnly : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveDuration = 0.5f; // Duration for the camera to move to the target position
    [SerializeField] private float yOffset = 5.0f; // Offset below the player
    private float playerYPosition;
    private Camera thisCamera;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMovingDown = false;
    private float moveStartTime;

    void OnEnable()
    {
        playerYPosition = transform.position.y;
        thisCamera = GetComponent<Camera>();

        if (thisCamera == null)
        {
            Debug.LogError("Camera component is not attached to the game object.");
        }
    }

    void Update()
    {
        if (thisCamera == null) return;

        float cameraBottomEdge = transform.position.y - thisCamera.orthographicSize;

        // Follow the player upwards if they move above the previous highest position
        if (player.position.y > playerYPosition)
        {
            playerYPosition = player.position.y;
            transform.position = new Vector3(transform.position.x, playerYPosition, transform.position.z);
            isMovingDown = false; // Stop any downward movement
        }
        // Follow the player downwards only if the player falls below the bottom edge of the camera view
        else if (player.position.y < cameraBottomEdge)
        {
            if (!isMovingDown)
            {
                isMovingDown = true;
                moveStartTime = Time.time;
                startPosition = transform.position;
                targetPosition = new Vector3(transform.position.x, player.position.y - yOffset, transform.position.z);
            }
        }

        // Smoothly move towards the target position over the specified duration
        if (isMovingDown)
        {
            float t = (Time.time - moveStartTime) / moveDuration;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            // Stop the movement once the target position is reached
            if (t >= 1.0f)
            {
                isMovingDown = false;
                playerYPosition = transform.position.y; // Update playerYPosition to the new position
            }
        }
    }
}
