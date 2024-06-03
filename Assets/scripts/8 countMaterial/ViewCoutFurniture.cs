using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewCoutFurniture : MonoBehaviour
{
    [SerializeField] private SpawnerFurniture _spawnerFurniture;
    [SerializeField] private TextMeshProUGUI _countFurniture;

    private void Start()
    {
        _spawnerFurniture.OnChageCountFurniture += () =>
        {
            _countFurniture.text = _spawnerFurniture.GetCountFurniture().ToString();
        };       
    }
}
