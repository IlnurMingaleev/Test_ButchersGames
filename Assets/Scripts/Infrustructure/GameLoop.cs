using System;
using ButchersGames;
using PickUps;
using Player;
using Runer;
using UI;
using UniRx;
using UnityEngine;

namespace Infrustructure
{
    public enum GameEndType
    {
        None = 0,
        Loosed = 1,
        Won = 2,
    }


    public interface IGameStateChangeable
    {
        void StartGame();
        void GameOver(GameEndType gameEndType);
        IReadOnlyReactiveProperty<GameEndType> GameEndStateType { get; }
    }

    public class GameLoop:MonoBehaviour,IGameStateChangeable
    {
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private GameObject _playerGO;
        [SerializeField] private CameraFollow _cameraFollow;
        [SerializeField] private GameObject _environmentGO;
        [SerializeField] private LevelObjectsSpawnController _levelObjectsSpawnController;
        

        [SerializeField] private Transform _windowRoot;
        [SerializeField] private Transform _nonActiveParent;
        
        private ReactiveProperty<GameEndType> _gameEndStateType = new ReactiveProperty<GameEndType>();
        private IWindowManager _windowManager;
        private PlayerController _playerController;
        private Camera _mainCamera;

        public IReadOnlyReactiveProperty<GameEndType> GameEndStateType => _gameEndStateType;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _windowManager = new WindowManager(_windowRoot, _nonActiveParent);
        }

        private void Start()
        {
            _windowManager.GetWindow<MainMenuPanel>().Init(_windowManager,this);
            _windowManager.Open<MainMenuPanel>();
            _levelObjectsSpawnController.SpawnAllLevelObjectsInFrustrum();
        }

        public void StartGame()
        {
            
            _levelManager.RestartLevel();
            
            _playerController =  _levelManager.CreatePlayer(_playerGO, _cameraFollow);
            _playerController.Init(_windowManager,this);
            _playerController.Wallet.MoneyCount.Subscribe((_) => HandleMoneyValueChange(_));
            
            _windowManager.GetWindow<PlayerPanel>().Init(_windowManager, _playerController.GetComponentInChildren<PlayerTag>(), _mainCamera);
            _windowManager.Open<PlayerPanel>();
                
            _environmentGO.SetActive(false);
            _windowManager.Close<MainMenuPanel>();
            InitPanels();
        }

        public void InitPanels()
        {
            _windowManager.CloseSelected(new Type[]
            {
                typeof(MainMenuPanel),
                typeof(WinPanel),
                typeof(LoosePanel),
            });

            SubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            _windowManager.GetWindow<LoosePanel>().Init(Restart,Exit, _windowManager);
            _windowManager.GetWindow<WinPanel>().Init(Continue,Exit, _windowManager);
        }
        private void HandleMoneyValueChange(int value)
        {
            if (value == 0) GameOver(GameEndType.Loosed);
        }

        private void Continue()
        {
            _windowManager.Close<WinPanel>();
            _levelManager.NextLevel();
            StartGame();
        }

        private void Restart()
        {
            _windowManager.Close<LoosePanel>();
            StartGame();
        }

        private void Exit()
        {
            _windowManager.CloseAll();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else            
            Application.Quit();
#endif
        }

        public void GameOver(GameEndType gameEndType)
        {
            SetEndLevelStateForPlayer();
            switch (gameEndType)
            {
                case GameEndType.Loosed: Loose(); break;
                case GameEndType.Won: Win(); break;
            }
        }

        private void SetEndLevelStateForPlayer()
        {
            _windowManager.Close<PlayerPanel>();
            _playerController.Animator.SetWalk(false);
            _playerController.PathFollower.enabled = false;
        }

        private void Loose()
        {
            _windowManager.Open<LoosePanel>();
        }

        private void Win()
        {
            _windowManager.Open<WinPanel>();
            _playerController.Animator.TriggerDance();
        }
        
    }
}