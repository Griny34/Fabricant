using TMPro;
using UnityEngine;

public class ViewCountFurniture : ViewCountMaterial
{
    [SerializeField] private TextMeshProUGUI _countFurniture;

    private void Start()
    {
        _stoolSpawner.OnChangedCount += () =>
        {
            _countDesk.text = _stoolSpawner.GetCountMaterial().ToString();
        };

        _stoolSpawner.OnChagedCountFurniture += () =>
        {
            _countFurniture.text = _stoolSpawner.GetCountFurniture().ToString();
        };
    }
}
