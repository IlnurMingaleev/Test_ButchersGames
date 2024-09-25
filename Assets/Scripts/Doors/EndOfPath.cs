using System;
using Enums;
using PickUps;
using Player;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class EndOfPath:MonoBehaviour
    {
        public event Action<GameEndEnum> EndGameEvent;
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.TryGetComponent(out PlayerTag playerTag))
            {
                playerTag.transform.parent.TryGetComponent(out PlayerAnimator animator);
                playerTag.transform.parent.TryGetComponent(out PlayerController playerController);
                animator.SetWalk(false);
                animator.TriggerDance();
                EndGameEvent?.Invoke(GameEndEnum.Won);
            }

            
        }
    }
}