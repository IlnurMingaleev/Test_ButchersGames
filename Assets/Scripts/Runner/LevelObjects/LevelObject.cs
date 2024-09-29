using Runner.Player;
using UnityEngine;

namespace Runner
{
    public enum LevelObjectType
    {
        None = 0,
        Bottle = 1,
        Money = 2
    }

    public class LevelObject : MonoBehaviour
    {
        [SerializeField] protected int _amount = 10;
        [SerializeField] private LevelObjectType levelObjectType;

        public LevelObjectType LevelObjectType => levelObjectType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerTag playerTag))
            {
                var playerController = playerTag.transform.parent.GetComponent<PlayerController>();
                RecountBalance(playerController);
                gameObject.SetActive(false);
            }
        }

        private void RecountBalance(PlayerController playerController)
        {
            switch (levelObjectType)
            {
                case LevelObjectType.Bottle:
                    playerController.Wallet.AddMoney(-_amount);
                    break;
                case LevelObjectType.Money:
                    playerController.Wallet.AddMoney(_amount);
                    break;
            }
        }
    }
}