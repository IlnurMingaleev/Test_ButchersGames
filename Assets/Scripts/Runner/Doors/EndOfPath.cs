using Runner.Player;
using UnityEngine;

namespace Runner
{
    public class EndOfPath : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerTag playerTag))
            {
                if (playerTag.transform.parent.TryGetComponent(out PlayerController playerController))
                    playerController.GameStateChanger.GameOver(GameEndType.Won);
                else
                    Debug.Log(
                        $"Player {other.name} entered end of path but didn't get {playerTag.transform.parent.name} with type : {typeof(PlayerController)}");
            }
            else
            {
                Debug.Log($"Player {other.name} entered end of path, but didn't get type : {typeof(PlayerTag)}");
            }
        }
    }
}