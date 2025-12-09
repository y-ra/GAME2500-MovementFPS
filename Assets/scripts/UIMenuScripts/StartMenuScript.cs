using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("You quit the game");
    }
}
