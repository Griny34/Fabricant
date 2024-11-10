using Agava.YandexGames;
using UnityEngine;

public class GameReadyServise : MonoBehaviour
{
    private void Awake()
    {
        if (Agava.WebUtility.WebApplication.IsRunningOnWebGL == false)
            return;

        GameReady();
    }

    private void GameReady()
    {
        YandexGamesSdk.GameReady();
    }
}
