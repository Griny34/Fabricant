using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private SpawnerFurniture _spawnerChair;

    private void OnEnable()
    {
        _spawnerChair.OnStartEffect += PlayEffect;
    }

    private void OnDisable()
    {
        _spawnerChair.OnStartEffect -= PlayEffect;
    }

    private void PlayEffect()
    {
        _particleSystem.Play();
    }
}
