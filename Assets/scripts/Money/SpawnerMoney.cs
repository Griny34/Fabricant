using UnityEngine;

namespace Currency
{
    public class SpawnerMoney : MonoBehaviour
    {
        [SerializeField] private BoxMoney _prefabMoney;
        [SerializeField] private Transform _positionSpawner;


        public void CreateMoney()
        {
            float randomX = Random.Range(-1f, 1f);
            float randomZ = Random.Range(-1f, 1f);

            Vector3 randomPosition = _positionSpawner.position + new Vector3(randomX, _positionSpawner.position.y, randomZ);

            Instantiate(_prefabMoney, randomPosition, Quaternion.identity);
        }
    }
}