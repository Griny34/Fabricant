using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFull : MonoBehaviour
{
    [SerializeField] private ParticleSystem _playerFull;
    [SerializeField] private Ñonveyor _conveyor;
    [SerializeField] private ShelfLeather _shelfLeather;
    [SerializeField] private ShelfWheel _shelfWheel;

    private void OnEnable()
    {
        _conveyor.OnFull += PlayParticle;
        _shelfLeather.OnFull += PlayParticle;
        _shelfWheel.OnFull += PlayParticle;
    }

    private void OnDisable()
    {
        _conveyor.OnFull -= PlayParticle;
        _shelfLeather.OnFull -= PlayParticle;
        _shelfWheel.OnFull -= PlayParticle;
    }

    private void PlayParticle()
    {
        _playerFull.Play();
    }
}
