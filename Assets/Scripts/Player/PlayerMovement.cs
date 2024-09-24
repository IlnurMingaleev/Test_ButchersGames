using System;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _sideMultiplier;

        public Transform PlayertTransform => _playerTransform;
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MovePlayer();
            }
        }

        private void MovePlayer()
        {
            float halfScreen = Screen.width / 2;
            float xPosition = (Input.mousePosition.x - halfScreen) / halfScreen;
            xPosition *= _sideMultiplier;
            xPosition = Math.Clamp(xPosition, -_sideMultiplier, _sideMultiplier);
            _playerTransform.localPosition = new Vector3(xPosition, 0, 0);
        }
    }
}