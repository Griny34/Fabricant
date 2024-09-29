using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private const string _tutorialScene = "TutorialScene";

    [SerializeField] private TextMeshProUGUI _tigerSaysText;
    [SerializeField] private TextMeshProUGUI[] _tigerSentenceArray;
    [SerializeField] private Button _nextSentenceButton;
    [SerializeField] private UserInterface _userInterface;

    //[SerializeField] private UnityEvent _onEndSay;

    private int _currentSayIndex;
    private float _delay = 1.2f;
    private int _value = 1;

    public static string Tiger => "Tiger";

    public static bool IsTiger => PlayerPrefs.GetInt(Tiger, 0) != 0;

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
                PlayerPrefs.SetInt(Tiger, _value);

                //_onEndSay?.Invoke();

                _userInterface.PlayGame();

                StartCoroutine(StartCoroutine());

                return;
            }

            _tigerSaysText.text = _tigerSentenceArray[_currentSayIndex].text;
        });
    }

    private IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(_tutorialScene);
    }
}
