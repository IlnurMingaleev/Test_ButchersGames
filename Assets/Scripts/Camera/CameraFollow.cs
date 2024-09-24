using UnityEngine;

namespace Camera
{
    public class CameraFollow:MonoBehaviour
    {
        private Transform _playerTransform;

        public void SetPlayerTransfrom(Transform playerTransform)
        {
            _playerTransform = playerTransform;
        }

        private void Update()
        {
            if (_playerTransform)
            {
                transform.position = _playerTransform.position;
                transform.rotation = _playerTransform.rotation;
            }

            
        }
    }
}