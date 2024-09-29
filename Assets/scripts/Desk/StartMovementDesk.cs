using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMovementDesk : MonoBehaviour
{
    [SerializeField] private float _speed;

    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.transform.TryGetComponent<PointOff>(out var pointOff) == true)
        {
            SwitchOffDesk();
        }
    }

    private void SwitchOffDesk()
    {
        gameObject.SetActive(false);
    }
}
