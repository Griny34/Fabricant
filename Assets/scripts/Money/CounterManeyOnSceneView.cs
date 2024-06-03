using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterManeyOnSceneView : MonoBehaviour
{
    [SerializeField] private CounterMoneyOnScene _counterMoneyOnScene;
    [SerializeField] private TextMeshProUGUI _textSalary;

    private void Start()
    {
        _counterMoneyOnScene.OnChangeVolue += () =>
        {
            _textSalary.text = _counterMoneyOnScene.GetVolue().ToString();
        };
    }
}
