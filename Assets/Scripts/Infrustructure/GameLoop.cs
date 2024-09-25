using System;
using ButchersGames;
using Camera;
using Enums;
using PathCreation.Examples;
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
        [SerializeField] private LoosePanelBase loseePanelBase;
        [SerializeField] private WinPanelBase winPanelBase;
        [SerializeField] private MainMenuPanelBase mainMenuPanelBase;
        private GameObject _currentPlayer;

        

        public void StartOnButtonClick()
        {
            
            OnDisable();
            _levelManager.Init();
            _levelManager.CurrentLevelInstance.EndOfPath.EndGameEvent += GameOver;
            _currentPlayer =  _levelManager.CreatePlayer(_playerGO, _cameraFollow);
            _environmentGO.SetActive(false);
            _currentPlayer.TryGetComponent(out PlayerController playerController);
            playerController.EndGameEvent += GameOver;
            mainMenuPanelBase.Close();
            SubscribeOnEvents();
            InitPanels();
        }

        public void InitPanels()
        {
            loseePanelBase.Close();
            winPanelBase.Close();
            mainMenuPanelBase.Close();

            SubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            loseePanelBase.OnExitEvent += Exit;
            loseePanelBase.OnRestartEvent += Restart;

            winPanelBase.OnExitEvent += Exit;
            winPanelBase.OnContinueEvent += Continue;
        }

        private void Continue()
        {
            winPanelBase.Close();
            _levelManager.NextLevel();
            StartOnButtonClick();
        }

        private void Restart()
        {
            loseePanelBase.Close();
            StartOnButtonClick();
        }

        private void Exit()
        {
            loseePanelBase.Close();
            winPanelBase.Close();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else            
            Application.Quit();
#endif
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
            loseePanelBase.Open();
            _currentPlayer.TryGetComponent(out PathFollower pathFollower);
            _currentPlayer.TryGetComponent(out PlayerAnimator playerAnimator);
            playerAnimator.SetWalk(false);
            pathFollower.enabled = false;
        }

        private void Win()
        {
            winPanelBase.Open();
            _currentPlayer.TryGetComponent(out PathFollower pathFollower);
            _currentPlayer.TryGetComponent(out PlayerAnimator playerAnimator);
            pathFollower.enabled = false;
            playerAnimator.SetWalk(false);
            playerAnimator.TriggerDance();
        }

        private void OnDisable()
        {
            if(_levelManager.CurrentLevelInstance!= null)_levelManager.CurrentLevelInstance.EndOfPath.EndGameEvent -= GameOver;
            
            loseePanelBase.OnExitEvent -= Exit;
            loseePanelBase.OnRestartEvent -= Restart;

            winPanelBase.OnExitEvent -= Exit;
            winPanelBase.OnContinueEvent -= Continue;
        }
    }
}