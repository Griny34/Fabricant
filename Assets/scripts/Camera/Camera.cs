using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Player;

public class Camera : MonoBehaviour
{
    [SerializeField] private JoystickPlayer _player;
    [SerializeField] private float _speed;

    [SerializeField] private Vector3 _startPosition;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 currentPosition = _player.transform.position + _startPosition;

        transform.position = Vector3.Lerp(transform.position, currentPosition, _speed * Time.deltaTime);
    }
}
