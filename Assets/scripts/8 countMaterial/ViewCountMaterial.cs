using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewCountMaterial : MonoBehaviour
{
    [SerializeField] protected SpawnerFurniture _stoolSpawner;
    [SerializeField] protected TextMeshProUGUI _countDesk;

    private void Start()
    {
        _stoolSpawner.OnChangeCount += () =>
        {
            _countDesk.text = _stoolSpawner.GetCountMatiriale().ToString();
        };
    }
}
