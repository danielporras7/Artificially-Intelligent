using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    public int currentLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public int GetLevelNumber()
    {
        return currentLevel;
    }

    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameOverStart()
    {
        //Waits a few seconds before sending the player to the game over screen
        StartCoroutine(Waiter());
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    //Waits for a few seconds before sending player to game over
    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(4f);

        //Play game over screen
        SendToGameOver();
    }

    //Sends player to game over screen
    public void SendToGameOver()
    {
        //Waits a few seconds before sending the player to the game over screen
        //StartCoroutine(Waiter());

        SceneManager.LoadSceneAsync(1);
    }


    // Start is called before the first frame update
    void Start()
    {
        //Don't get the current active scene if you are in the game over screen
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {

        }

        else
        {
            currentLevel = SceneManager.GetActiveScene().buildIndex;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Don't get the current active scene if you are in the game over screen
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {

        }

        else
        {
            currentLevel = SceneManager.GetActiveScene().buildIndex;
        }
    }
}
