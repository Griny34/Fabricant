using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

public class Localization : MonoBehaviour
{
    private const string RussianCode = "Russian";
    private const string EnglishCode = "English";
    private const string TurkishCode = "Turkish";
    private const string Russian = "ru";
    private const string English = "en";
    private const string Turkish = "tr";

    [SerializeField] private LeanLocalization _leanLenguage;

    private void Awake()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ChangeLanguage();
#endif
    }

    private void ChangeLanguage()
    {
        string lenguageCode = YandexGamesSdk.Environment.i18n.lang;

        switch (lenguageCode)
        {
            case Russian:
                _leanLenguage.SetCurrentLanguage(RussianCode);
                break;
            case English:
                _leanLenguage.SetCurrentLanguage(EnglishCode);
                break;
            case Turkish:
                _leanLenguage.SetCurrentLanguage(TurkishCode);
                break;
        }
    }
}
