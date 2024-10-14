using TMPro;
using UnityEngine;

namespace Factory
{
    public class EaseViewICreate : MonoBehaviour
    {
        [SerializeField] private EasyFactory _factory;
        [SerializeField] private TextMeshProUGUI _countMateriale;

        private void OnEnable()
        {
            _factory.OnChangedCount += ChangeTextMateriale;
        }

        private void OnDisable()
        {
            _factory.OnChangedCount -= ChangeTextMateriale;
        }

        private void ChangeTextMateriale()
        {
            if(_countMateriale == null)
                return;

            _countMateriale.text = _factory.GetCountMateriale().ToString();
        }
    }
}