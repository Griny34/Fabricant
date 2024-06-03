using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewCountFurniture : ViewCountMaterial
{
    [SerializeField] private TextMeshProUGUI _countFurniture;

    private void Start()
    {
        _stoolSpawner.OnChangeCount += () =>
        {
            _countDesk.text = _stoolSpawner.GetCountMatiriale().ToString();          
        };

        _stoolSpawner.OnChageCountFurniture += () =>
        {
            _countFurniture.text = _stoolSpawner.GetCountFurniture().ToString();
        };
    }
}
