using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairConstructionArea : MonoBehaviour
{
    [SerializeField] private ChairArea[] _chairsArea;

    public static ChairConstructionArea Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnTriggerStay(Collider collider)
    {
        if(collider.transform.TryGetComponent<MovementPlayer>(out var movementPlayer) == true)
        {
            foreach (var chairSpawner in _chairsArea)
            {
                // logic
                movementPlayer.GetComponent<BoardsPlayer>().SetTargetPoint(chairSpawner.transform);
            }

            movementPlayer.GetComponent<BoardsPlayer>().SetTargetPoint(null);
        }
    }
}
