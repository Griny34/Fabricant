using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _money2;

    private void Start()
    {
        _enemy.OnMoneyChanged += value =>
        {
            UpdateUI();
        };

        UpdateUI();
    }

    private void UpdateUI()
    {
        _money.text = _enemy.GetMoneyEnemy().ToString();
        _money2.text = _enemy.GetMoneyEnemy().ToString();
    }
}
