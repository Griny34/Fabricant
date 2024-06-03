using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairArea : MonoBehaviour
{
    public event Action MoveChair;

    private void OnTriggerStay(Collider collider)
    {
        if(collider.transform.TryGetComponent<MovementPlayer>(out var movementPlayer) == true)
        {
            MoveChair?.Invoke();
        }
    }
}
