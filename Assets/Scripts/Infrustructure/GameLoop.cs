using System;
using ButchersGames;
using Camera;
using UnityEngine;

namespace Infrustructure
{
    public class GameLoop:MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private GameObject _playerGO;
        [SerializeField] private CameraFollow _cameraFollow; 
        
        private void Start()
        {
            _levelManager.Init();
            _levelManager.CreatePlayer(_playerGO, _cameraFollow);
        }
    }
}