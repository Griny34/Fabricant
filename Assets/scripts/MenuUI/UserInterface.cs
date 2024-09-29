using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterface : MonoBehaviour
{
    private const string _playScene = "PlayScene";
    private const string _tutorialScene = "TutorialScene";

    [SerializeField] private PauseController _controlerPause;

    public void PlayGame() =>
        SceneManager.LoadScene(Tutorial.IsTiger ? _playScene : _tutorialScene);

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
