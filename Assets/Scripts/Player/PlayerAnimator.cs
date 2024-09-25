using System;
using UnityEngine;

namespace Player
{
    public class PlayerAnimator: MonoBehaviour
    {
        public static readonly int Walking = Animator.StringToHash("Walking");
        public static readonly int Dance = Animator.StringToHash("Dance");
        [SerializeField] private Animator _animator;

        private void Start()
        {
            SetWalk(true);
        }

        public void SetWalk(bool value)
        {
            _animator.SetBool(Walking,value);
        }

        public void TriggerDance()
        {
            _animator.SetTrigger(Dance);
        }
    }
}