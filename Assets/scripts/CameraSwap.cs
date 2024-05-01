using UnityEngine;

public class CameraTriggerSwitch : MonoBehaviour
{
    public GameObject cameraToActivateGameObject; // Reference to the GameObject of the camera to activate
    public GameObject cameraToDeactivateGameObject; // Reference to the GameObject of the original camera to deactivate

    private bool hasBeenTriggered = false; // Flag to check if the trigger has been activated

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasBeenTriggered) // Make sure your player has a tag named "Player"
        {
            // Toggle the camera only if it has not been triggered before
            cameraToDeactivateGameObject.SetActive(!cameraToDeactivateGameObject.activeSelf);
            cameraToActivateGameObject.SetActive(!cameraToActivateGameObject.activeSelf);

            hasBeenTriggered = true; // Set the flag to true to prevent reactivation
        }
    }
}