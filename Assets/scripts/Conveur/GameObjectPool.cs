using Gameplay.Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameObjectPool : MonoBehaviour
{
    [SerializeField] private TriggerHandler _playerTrigger;
    [SerializeField] private JoystickPlayer _player;
    [SerializeField] private DeskInventory _deskInventory;

    [SerializeField] private Transform _contaner;
    [SerializeField] private int _capasity;
    
    private List<Desk> _pool = new List<Desk>();
    private Desk _relevantDesk;
    private Desk _deskPrefab;

    private void Start()
    {
        _playerTrigger.OnEnter += col =>
        {
            if (col.GetComponent<JoystickPlayer>() == null) return;

            OutDesk();
        };

        _playerTrigger.OnExit += col =>
        {
            if (col.GetComponent<JoystickPlayer>() == null) return;
            
            if(_relevantDesk != null)
            {
                _relevantDesk.OnFinished -= OutDesk;
            }
        };

        OnStart();
    }

    protected virtual void OnStart() { }

    protected void Initialize(Desk prefab)
    {
        _deskPrefab = prefab;

        for(int i = 0; i < _capasity; i++)
        {
            Spawn(prefab);
        }
    }

    private void Spawn(Desk prefab)
    {
        Desk spawned = Instantiate(prefab, _contaner);

        spawned.gameObject.SetActive(false);

        _pool.Add(spawned);
    }

    protected bool TryGetGameObject(out Desk result)
    {
        result = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        return result;
    }

    private Desk GetRelevantDesk()
    {
        if (_pool.Count == 0)
        {
            return null;
        }

        float minDistance = Mathf.Infinity;
        int nearestDeskIndex = 0;
        int indexCounter = 0;

        foreach (Desk desk in _pool)
        {
            float distance = Vector3.Distance(desk.transform.position, _player.transform.position);

            if (distance < minDistance)
            {
                nearestDeskIndex = indexCounter;
                minDistance = distance;
            }

            indexCounter++;
        }

        return _pool[nearestDeskIndex];
    }

    private void OutDesk()
    {
        if (_deskInventory.IsFull) return;

        _relevantDesk = GetRelevantDesk();
        _deskInventory.AddItem(_relevantDesk);
        _pool.Remove(_relevantDesk);

        _relevantDesk.OnFinished += OutDesk;

        Spawn(_deskPrefab);
    }
}
