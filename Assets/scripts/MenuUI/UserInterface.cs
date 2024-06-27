using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private ControlerPause _controlerPause;

    public void PlayGame() =>
        SceneManager.LoadScene(Tutorial.IsTiger ? "PlayScene" : "TutorialScene");

    public void LoadStartMenu() =>
        SceneManager.LoadScene(0);
    
    public void LoadMenu() =>
        SceneManager.LoadScene(1);

    public void ExitGame() =>
        Application.Quit();

    public void StopGame() =>
        _controlerPause.IsPaused = true;

    public void ResumeGame() =>
        _controlerPause.IsPaused = false;

    public void ClearSave() =>
        PlayerPrefs.DeleteAll();
}
