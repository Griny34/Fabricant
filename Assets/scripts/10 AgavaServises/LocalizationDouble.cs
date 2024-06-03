using Agava.YandexGames;
using Lean.Localization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationDouble : MonoBehaviour
{
    private const string RussianCode = "Russian";
    private const string EnglishCode = "English";
    private const string TurkishCode = "Turkish";
    private const string Russian = "ru";
    private const string English = "en";
    private const string Turkish = "tr";

    [SerializeField] private LeanLocalization _leanLenguage;
    [SerializeField] private LeanLocalization _leanLenguage2;

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
                _leanLenguage2.SetCurrentLanguage(RussianCode);
                break;
            case English:
                _leanLenguage.SetCurrentLanguage(EnglishCode);
                _leanLenguage2.SetCurrentLanguage(EnglishCode);
                break;
            case Turkish:
                _leanLenguage.SetCurrentLanguage(TurkishCode);
                _leanLenguage2.SetCurrentLanguage(TurkishCode);
                break;
        }
    }
}
