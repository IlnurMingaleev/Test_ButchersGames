using System;
using ButchersGames;
using Camera;
using UnityEditor;
using UnityEngine;

namespace Infrustructure
{
    public class GameLoop:MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private GameObject _playerGO;
        [SerializeField] private CameraFollow _cameraFollow;
        private GameObject _currentPlayer;

        public void StartOnButtonClick()
        {
            _levelManager.Init();
            _currentPlayer =  _levelManager.CreatePlayer(_playerGO, _cameraFollow);
        }
    }
}