using TMPro;
using UnityEngine;

public class ViewCountMaterial : MonoBehaviour
{
    [SerializeField] protected SpawnerFurniture _stoolSpawner;
    [SerializeField] protected TextMeshProUGUI _countDesk;

    private void Start()
    {
        _stoolSpawner.OnChangedCount += () =>
        {
            _countDesk.text = _stoolSpawner.GetCountMaterial().ToString();
        };
    }
}
