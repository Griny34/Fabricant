using TMPro;
using UnityEngine;

namespace Factory
{
    public class MidleViewFactory : MonoBehaviour
    {
        [SerializeField] private MidleFactory _factory;
        [SerializeField] private TextMeshProUGUI _countFurniture;

        private void OnEnable()
        {
            _factory.OnChagedCountFurniture += ChangeTextFurniture;
        }

        private void OnDisable()
        {
            _factory.OnChagedCountFurniture -= ChangeTextFurniture;
        }

        private void ChangeTextFurniture()
        {
            if (_countFurniture == null)
                return;

            _countFurniture.text = _factory.GetCountFurniture().ToString();
        }
    }
}