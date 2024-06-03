using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class Desk : Item
{
    private void Start()
    {
        _target = PointOff.Instance.transform;
    }
}
