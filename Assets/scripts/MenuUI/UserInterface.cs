using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    public void PlayGame()
    {
        if (Tutorial.IsTiger)
        {
            SceneManager.LoadScene("PlayScene");
        }
        else
        {
            SceneManager.LoadScene("TutorialScene");
        }
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
