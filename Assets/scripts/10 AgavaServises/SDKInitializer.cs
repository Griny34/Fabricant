using Agava.YandexGames;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
        //{
        //    yield break;
        //}           

        Debug.Log("start");
        yield return YandexGamesSdk.Initialize(Initializer);
    }

    private void Initializer()
    {
        //if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
        //    return;

        LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang switch
        {
            "ru" => "Russian",
            "tr" => "Turkish",
            "en" => "English"
        });

        _loadText.enabled = true;

        SceneManager.LoadScene("MenuScene");
    }
}
