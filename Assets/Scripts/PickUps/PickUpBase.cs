using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace PickUps
{
    public class PickUpBase : MonoBehaviour
    {
        [SerializeField] protected int _amount = 10;
        [SerializeField] private PickUpTypeEnum _pickUpType;
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.TryGetComponent(out PlayerTag playerTag))
            {
                playerTag.transform.parent.TryGetComponent(out PlayerController playerController);
                RecountBalance(playerController);
            }

            Destroy(gameObject);
        }

        private void RecountBalance(PlayerController playerController)
        {
            switch (_pickUpType)
            {
                case PickUpTypeEnum.Bottle:
                    MinusMoney(playerController);
                    break;
                case PickUpTypeEnum.Money:
                    AddMoney(playerController);
                    break;
            }
        }

        private void AddMoney(PlayerController playerController)
        {
            playerController.AddMoney(_amount);
        }
        private void MinusMoney(PlayerController playerController)
        {
            playerController.MinusMoneyCount(_amount);
            
        }
    }

    public enum PickUpTypeEnum
    {
        None = 0,
        Bottle = 1,
        Money = 2,
    }
}