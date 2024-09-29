using UnityEngine;

namespace Runner
{
    public class CameraFollow : MonoBehaviour
    {
        private Transform _playerTransform;

        private void Update()
        {
            if (_playerTransform)
            {
                transform.position = _playerTransform.position;
                transform.rotation = _playerTransform.rotation;
            }
        }

        public void SetPlayerTransfrom(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }
    }
}