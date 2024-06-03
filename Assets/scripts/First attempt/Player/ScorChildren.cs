using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScorChildren : MonoBehaviour
{
    public static ScorChildren Instance { get; private set; }
    [SerializeField] private LeftHand _leftHand;
    [SerializeField] private float _gap;

    private int _childrenCount;
    public int ChildrenCount
    { 
        get => _childrenCount;
        private set
        {
            _childrenCount = value;

            OnChildrenCountChanged?.Invoke(_childrenCount);
        }
    }

    public event Action<int> OnChildrenCountChanged;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        ChildrenCount = 2;
    }

    private void Update()
    {
        AcceptBoards();
        GiveAwayBoards();
    }

    public void AcceptBoards()
    {
        /*if (ChildrenCount < transform.childCount)
        {
            ChildrenCount++;
            _leftHand.transform.position += new Vector3(0, _gap, 0);
        }*/

        ChildrenCount++;
        _leftHand.transform.position += new Vector3(0, _gap, 0);
    }

    private void GiveAwayBoards()
    {
        if (ChildrenCount > transform.childCount)
        {
            ChildrenCount--;
            _leftHand.transform.position += new Vector3(0, -_gap, 0);
        }
    }
}
