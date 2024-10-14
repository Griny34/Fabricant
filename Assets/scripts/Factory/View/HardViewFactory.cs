using TMPro;
using UnityEngine;

namespace Factory
{
    public class HardViewFactory : MonoBehaviour
    {
        [SerializeField] private HardFactory _factory;
        [SerializeField] private TextMeshProUGUI _countMateriale;
        [SerializeField] private TextMeshProUGUI _countFurniture;

        private void OnEnable()
        {
            _factory.OnChangedCount += ChangeTextMateriale;
            _factory.OnChagedCountFurniture += ChangeTextFurniture;
        }

        private void OnDisable()
        {
            _factory.OnChangedCount -= ChangeTextMateriale;
            _factory.OnChagedCountFurniture -= ChangeTextFurniture;
        }

        private void ChangeTextMateriale()
        {
            if (_countMateriale == null)
                return;

            _countMateriale.text = _factory.GetCountMateriale().ToString();
        }

        private void ChangeTextFurniture()
        {
            if (_countFurniture == null)
                return;

            _countFurniture.text = _factory.GetCountFurniture().ToString();
        }
    }
}