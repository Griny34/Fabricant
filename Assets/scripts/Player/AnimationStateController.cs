using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;

    //private float _velocity = 0;
    private int _velocityHash;

    private void Start()
    {
        _velocityHash = Animator.StringToHash("Velocity");
    }

    private void Update()
    {
        
    }
}
