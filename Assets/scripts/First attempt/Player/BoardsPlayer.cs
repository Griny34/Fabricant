using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BoardsPlayer : MonoBehaviour
{
    [SerializeField] private ChairArea _chairArea;
    //[SerializeField] private SpawnerChair _spawnerChair;
    [SerializeField] private float _speed;

    private Stack<Chair—reationMovements> _boardsPlayer = new Stack<Chair—reationMovements>();
    private Coroutine coroutine;
    private float count;
    private bool isGo = false;

    public Transform TargetPoint { get; private set; }

    private void Awake()
    {
        count = _boardsPlayer.Count;
    }

    public void SetTargetPoint(Transform point)
    {
        TargetPoint = point;
    }

    private void OnTriggerStay(Collider collider)
    {

        if (collider.transform.TryGetComponent<ChairConstructionArea>(out var chairConstructionArea) == true)
        {

            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }

                if (_boardsPlayer.Count != 0 && _boardsPlayer.Peek() != null && isGo == false)
                {
                    coroutine = StartCoroutine(GoBoards(_boardsPlayer.Peek()));
                    isGo = false;
                }
            }
        }
    }

    public void AddBoard(Chair—reationMovements chairConstructionArea)
    {
        _boardsPlayer.Push(chairConstructionArea);
    }

    public void RemoveBoard()
    {
        if (_boardsPlayer.Count > 0)
        {
            _boardsPlayer.Pop();
        }
    }

    private IEnumerator GoBoards(Chair—reationMovements nextBoard)
    {
        isGo = true;
        nextBoard.transform.SetParent(null);
        Debug.Log(gameObject.transform.childCount);
        while (true)
        {
            //nextBoard.transform.position = Vector3.MoveTowards(nextBoard.transform.position, _spawnerChair.transform.position, _speed * Time.deltaTime);

            //if (nextBoard.transform.position == _spawnerChair.transform.position)
            //{
            //    isGo = false;
            //    RemoveBoard();
            //    Destroy(nextBoard.gameObject);
                //yield break;
            //}
            yield return null;
        }
    }
}
