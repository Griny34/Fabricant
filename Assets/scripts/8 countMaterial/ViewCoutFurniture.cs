using TMPro;
using UnityEngine;

public class ViewCoutFurniture : MonoBehaviour
{
    [SerializeField] private SpawnerFurniture _spawnerFurniture;
    [SerializeField] private TextMeshProUGUI _countFurniture;

    private void Start()
    {
        _spawnerFurniture.OnChagedCountFurniture += () =>
        {
            _countFurniture.text = _spawnerFurniture.GetCountFurniture().ToString();
        };
    }
}
