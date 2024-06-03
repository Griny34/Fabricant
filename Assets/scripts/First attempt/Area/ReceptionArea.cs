using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReceptionArea : MonoBehaviour
{
    [SerializeField] private int _maxChildrenCount = 11;
    [SerializeField] private GameObject _area;
    [SerializeField] private ScorChildren _childrenCount;

    [SerializeField] private UnityEvent OnFull;

    private void Start()
    {
        _area.SetActive(false);

        ScorChildren.Instance.OnChildrenCountChanged += v =>
        {
            if (v <= _maxChildrenCount) return;

            OnFull?.Invoke();
        };
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.TryGetComponent<MovementPlayer>(out var player) == true && _childrenCount.ChildrenCount < 11)
        {
            _area.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.transform.TryGetComponent<MovementPlayer>(out var player) == true && _childrenCount.ChildrenCount >= 11)
        {        
            _area.SetActive(false);            
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.transform.TryGetComponent<MovementPlayer>(out var player) == true)
        {
            _area.SetActive(false);
        }
    }
}
