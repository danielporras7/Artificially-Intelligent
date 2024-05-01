using UnityEngine;

public class Visibility : MonoBehaviour
{
    public Animator animator;  // Reference to the Animator component
    private Camera mainCamera;
    private bool animationStarted = false;

    void Start()
    {
        mainCamera = Camera.main;  // Get the main camera
    }

    void Update()
    {
        // Perform a check to see if the object is in front of the camera and within the viewport
        if (!animationStarted && GetComponent<Renderer>().isVisible)
        {
            animator.SetBool("StartAnimation", true);
            animationStarted = true;  // Set the flag to true so animation is only started once
        }
    }
}
