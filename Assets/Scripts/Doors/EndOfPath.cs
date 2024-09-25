using PickUps;
using Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class EndOfPath:MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            
            if (other.TryGetComponent(out PlayerTag playerTag))
            {
                playerTag.transform.parent.TryGetComponent(out PlayerAnimator animator);
                playerTag.transform.parent.TryGetComponent(out PlayerController playerController);
                animator.SetWalk(false);
                animator.TriggerDance();
                

            }

            
        }
    }
}