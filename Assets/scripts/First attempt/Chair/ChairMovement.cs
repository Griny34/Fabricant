using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private ChairArea _area;

    private void OnEnable()
    {
        _area.MoveChair += TakeChair;
    }

    private void OnDisable()
    {
        _area.MoveChair -= TakeChair;
    }

    public void TakeChair()
    {
        transform.position = Vector3.MoveTowards(transform.position, RightHand.Instance.transform.position, _speed * Time.deltaTime);
    }
}
