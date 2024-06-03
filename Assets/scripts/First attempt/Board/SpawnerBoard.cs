using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBoard : MonoBehaviour
{
    [SerializeField] private Transform _pointSpawner;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _delay;

    private GameObject _board;
    private Coroutine _coroutine;

    private void Start()
    {
        if( _coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(SpawnEndless());
    }

    private void CreatBoard()
    {
        _board = Instantiate(_prefab, _pointSpawner.position, Quaternion.identity);
    }

    private IEnumerator SpawnEndless()
    {
        while (true)
        {
            CreatBoard();
            yield return new WaitForSeconds(_delay);
        }
    }

}
