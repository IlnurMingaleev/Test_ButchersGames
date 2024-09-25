using ButchersGames;
using Camera;
using Enums;
using Player;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrustructure
{
    public class GameLoop:MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private GameObject _playerGO;
        [SerializeField] private CameraFollow _cameraFollow;
        [SerializeField] private GameObject _environmentGO;
        
        //UI
        [FormerlySerializedAs("_loseePanel")] [SerializeField] private LoosePanelBase loseePanelBase;
        [FormerlySerializedAs("_winPanel")] [SerializeField] private WinPanelBase winPanelBase;
        [FormerlySerializedAs("_mainMenuPanel")] [SerializeField] private MainMenuPanelBase mainMenuPanelBase;
        private GameObject _currentPlayer;

        

        public void StartOnButtonClick()
        {
            
            _levelManager.Init();
            _currentPlayer =  _levelManager.CreatePlayer(_playerGO, _cameraFollow);
            _environmentGO.SetActive(false);
            _currentPlayer.TryGetComponent(out PlayerController playerController);
            playerController.EndGameEvent += GameOver;
        }
        
        private void GameOver(GameEndEnum gameEndEnum)
        {
            switch (gameEndEnum)
            {
                case Enums.GameEndEnum.Loosed:
                    Loose();
                    break;
                case Enums.GameEndEnum.Won: 
                    Win();
                    break;
            }
        }
        private void Loose()
        {
            
        }

        private void Win()
        {
            
        }
    }
}