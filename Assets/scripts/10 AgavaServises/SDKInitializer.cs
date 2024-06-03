using Agava.YandexGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SDKInitializer : MonoBehaviour
{
    private void Awake()
    {
        if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
            return;

        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
        {
            yield break;
        }           

        Debug.Log("start");
        yield return YandexGamesSdk.Initialize(Initializer);
    }

    private void Initializer()
    {
        if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
            return;

        SceneManager.LoadScene("MenuScene");
    }
}
