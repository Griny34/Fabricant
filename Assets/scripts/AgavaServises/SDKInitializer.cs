using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Lean.Localization;
using Agava.YandexGames;

public class SDKInitializer : MonoBehaviour
{
    [SerializeField] private TMP_Text _loadText;
    [SerializeField] private string _editorLanguageOnInit = "Russian";

    private void Awake()
    {
        if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
        {
            LeanLocalization.SetCurrentLanguageAll(_editorLanguageOnInit);
            SceneManager.LoadScene("MenuScene");
            return;
        }      

        YandexGamesSdk.CallbackLogging = true;
        _loadText.enabled = false;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(Initializer);
    }

    private void Initializer()
    {
        LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang switch
        {
            "ru" => "Russian",
            "tr" => "Turkish",
            _ => "English"
        });

        _loadText.enabled = true;

        SceneManager.LoadScene("MenuScene");
    }
}