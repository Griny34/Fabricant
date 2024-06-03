using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class LaborMovement : MonoBehaviour
{  
    [SerializeField] private float _speed;

    private MovmentBoard _movementBoard;
    private Coroutine _coroutine;

    private void Start()
    {
        _movementBoard = GetComponent<MovmentBoard>();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.TryGetComponent<PointTaking>(out var pointTaking) == true && LeftHand.Instance.ActiveLaborMovement == null)
        {
            if (_coroutine != null)
            {
                StopCoroutine(Move()); 
            }

            gameObject.transform.SetParent(MovementPlayer.Instance.transform, true);
            LeftHand.Instance.SetActiveBoart(this);
            _coroutine = StartCoroutine(Move());
            _movementBoard.enabled = false;
        }
    }

    private IEnumerator Move()
    {
        while(transform.position != LeftHand.Instance.transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, LeftHand.Instance.transform.position, _speed * Time.deltaTime);
            yield return null;   
            //yield break;
        }
        Debug.Log(name);
        ScorChildren.Instance.AcceptBoards();
    }
}
