using PickUps;
using Player;
using UnityEngine;

namespace Doors
{
    public class DoorBase:MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        public static readonly int Open = Animator.StringToHash("Open");

        private void Start()
        {
            _animator.SetBool(Open,false);
        }

        private void OnTriggerEnter(Collider other)
        {
            
            if (other.TryGetComponent(out PlayerTag playerTag))
            {
                //playerTag.transform.parent.TryGetComponent(out PlayerController playerController);
                _animator.SetBool(Open,true);
            }

            
        }

    }
}