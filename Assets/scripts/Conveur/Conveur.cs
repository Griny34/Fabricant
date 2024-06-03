using Gameplay.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveur : GameObjectPool
{
    [SerializeField] private Desk _prefabDesk;
    [SerializeField] private Transform _pointSpawne;
    [SerializeField] private float _delay;      

    private float _elepsedTime = 0;

    protected override void OnStart()
    {
        Initialize(_prefabDesk);
    }

    private void Update()
    {
        _elepsedTime += Time.deltaTime;

        if( _elepsedTime >= _delay)
        {           
            if (TryGetGameObject(out Desk desk))
            {
                _elepsedTime = 0;

                SetDesk(desk, _pointSpawne.position);
            }
        }
    }

    private void SetDesk(Desk desk, Vector3 spawnPosition)
    {
        desk.gameObject.SetActive(true);
        desk.transform.position = spawnPosition;
        desk.StartMove(PointOff.Instance.transform);
    }
}
