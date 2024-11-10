using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UpgradeSkills;

namespace Factory
{
    public class StackMaterial : MonoBehaviour
    {
        private const string _maxCountDeskKey = "MaxCountDesk";

        [SerializeField] private Transform _pointStartStack;
        [SerializeField] private int _maxCountDesk;
        [SerializeField] private int _countDeskForCreatChair;
        [SerializeField] private Upgrade _upgrade;

        private List<Material> _inventoryMateriale = new List<Material>();
        private float _number = 0;
        private int _startCountDesk = 3;
        private float _delay = 0.5f;
        private float _powerJump = 1;
        private int _numberJumps = 1;
        private int _angleRotation = 90;
        private float _interval = 0.2f;
        private float _startPoint = 0.025f;

        public bool IsFull => _inventoryMateriale.Count >= _maxCountDesk;

        private void Awake()
        {
            if (PlayerPrefs.HasKey(_maxCountDeskKey))
            {
                _maxCountDesk = PlayerPrefs.GetInt(_maxCountDeskKey);
            }
            else
            {
                _maxCountDesk = _startCountDesk;
            }
        }

        private void OnEnable()
        {
            _upgrade.OnBuyDeskInventory += UpgradeStack;
        }

        private void OnDisable()
        {
            _upgrade.OnBuyDeskInventory -= UpgradeStack;
        }

        private void UpgradeStack()
        {
            _maxCountDesk++;
            PlayerPrefs.SetInt(_maxCountDeskKey, _maxCountDesk);
        }

        public void AddMaterial(Material material)
        {
            _inventoryMateriale.Add(material);

            material.transform
                .DOJump(
                _pointStartStack.position + new Vector3(0, _startPoint + _number, 0),
                _powerJump, _numberJumps,
                _delay)
                .OnComplete(() =>
                {
                    material.transform.SetParent(_pointStartStack.transform, true);
                    material.transform.localPosition = new Vector3(0, _startPoint + _number, 0);
                    material.transform.localRotation = Quaternion.Euler(new Vector3(0, _angleRotation, 0));
                    _number += _interval;
                });
        }

        public void RemoveDesk(Material material, Transform pointDestroy)
        {
            if (material == null) return;

            material.transform.SetParent(null);

            material.transform.DOJump(
                pointDestroy.position,
                _powerJump, _numberJumps,
                _delay).OnComplete(() =>
            {
                _inventoryMateriale.Remove(material);
                Destroy(material.gameObject);
                _number -= _interval;
            });
        }

        public List<Material> GetListMaterial()
        {
            return _inventoryMateriale;
        }

        public Material GetLastDesk()
        {
            if (_inventoryMateriale.Count <= 0)
            {
                return null;
            }

            return _inventoryMateriale[_inventoryMateriale.Count - 1];
        }
    }
}