using Runner.Player;
using UnityEngine;

namespace Runner
{
    public class Door : MonoBehaviour
    {
        public static readonly int Open = Animator.StringToHash("Open");
        [SerializeField] private Animator _animator;

        private void Start()
        {
            _animator.SetBool(Open, false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerTag playerTag))
                //playerTag.transform.parent.TryGetComponent(out PlayerController playerController);
                _animator.SetBool(Open, true);
        }
    }
}