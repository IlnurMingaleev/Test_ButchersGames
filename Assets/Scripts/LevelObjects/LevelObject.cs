using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace PickUps
{
    public enum LevelObjectType
    {
        None = 0,
        Bottle = 1,
        Money = 2,
    }
    public class LevelObject : MonoBehaviour
    {
        [SerializeField] protected int _amount = 10;
        [SerializeField] private LevelObjectType levelObjectType;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerTag playerTag))
            {
                PlayerController playerController = playerTag.transform.parent.GetComponent<PlayerController>();
                RecountBalance(playerController);
            }
            //TODO use object pool
            Destroy(gameObject);
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