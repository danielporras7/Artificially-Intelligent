using UnityEngine;

public class CameraYOnly : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float playerYPosition;

    void OnEnable()
    {
        playerYPosition = transform.position.y;
    }

    void Update()
    {
        // This allows the camera to only follow the player up vertically
        if (player.position.y > playerYPosition)
        {
            playerYPosition = player.position.y;
            transform.position = new Vector3(transform.position.x, playerYPosition, transform.position.z);
        }
    }
}