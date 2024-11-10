using UnityEngine;
using Factory;

public class StartEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private EasyFactory _spawnerChair;

    private void OnEnable()
    {
        _spawnerChair.OnStartedEffect += PlayEffect;
    }

    private void OnDisable()
    {
        _spawnerChair.OnStartedEffect -= PlayEffect;
    }

    private void PlayEffect()
    {
        _particleSystem.Play();
    }
}
