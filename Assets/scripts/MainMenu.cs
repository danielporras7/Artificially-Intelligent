using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void MainGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
