using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public static string Tiger => "Tiger";
    public static bool IsTiger => PlayerPrefs.GetInt(Tiger, 0) != 0;

    [SerializeField] private TextMeshProUGUI _tigerSaysText;
    [SerializeField] private TextMeshProUGUI[] _tigerSentenceArray;
    [SerializeField] private Button _nextSentenceButton;
    [SerializeField] private UnityEvent _onEndSay;

    private int _currentSayIndex;
    private float _delay = 1.2f;

    private void Start()
    {
        Creatizing();
    }

    private void Creatizing()
    {
        _tigerSaysText.text = _tigerSentenceArray[_currentSayIndex].text;

        _nextSentenceButton.onClick.AddListener(() =>
        {
            if (_currentSayIndex >= _tigerSentenceArray.Length)
                return;

            _currentSayIndex++;

            if (_currentSayIndex >= _tigerSentenceArray.Length)
            {
                PlayerPrefs.SetInt(Tiger, 1);

                _onEndSay?.Invoke();

                StartCoroutine(StartCoroutine());

                return;
            }

            _tigerSaysText.text = _tigerSentenceArray[_currentSayIndex].text;
        });
    }

    private IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene("TutorialScene");
    }
}
