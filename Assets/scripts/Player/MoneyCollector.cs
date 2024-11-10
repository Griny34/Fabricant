using UnityEngine;
using WalletUser;
using Currency;

namespace Player
{
    public class MoneyCollector : MonoBehaviour
    {
        [SerializeField] private Money _money;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnTriggerEnter(Collider collider)
        {
           if (collider.gameObject.TryGetComponent<BoxMoney>(out BoxMoney boxMoney) == true)
            {
                _wallet.TakeMoney(_money.GetMoneyValue());

                _wallet.TakeSalary(_money.GetMoneyValue());

                _particleSystem.Play();

                Destroy(boxMoney.gameObject, 0.2f);
            }
        }
    }
}