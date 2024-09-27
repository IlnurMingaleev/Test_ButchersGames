using System;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _sideMultiplier;

        private float halfScreen;
        public Transform PlayertTransform => _playerTransform;

        private void Awake()
        {
            halfScreen = Screen.height / 2;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MovePlayer();
            }
        }

        private void MovePlayer()
        {
            float xPosition = (Input.mousePosition.x - halfScreen) / halfScreen;
            xPosition *= _sideMultiplier;
            xPosition = Math.Clamp(xPosition, -_sideMultiplier, _sideMultiplier);
            _playerTransform.localPosition = new Vector3(xPosition, 0, 0);
        }
    }
}