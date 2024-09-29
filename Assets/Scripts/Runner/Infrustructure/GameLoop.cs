using Runner.Player;
using Runner.UI;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace Runner
{
    public enum GameEndType
    {
        None = 0,
        Loosed = 1,
        Won = 2
    }


    public interface IGameStateChangeable
    {
        IReadOnlyReactiveProperty<GameEndType> GameEndStateType { get; }
        void StartGame();
        void GameOver(GameEndType gameEndType);
    }

    public class GameLoop : MonoBehaviour, IGameStateChangeable
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private GameObject playerGo;
        [SerializeField] private CameraFollow cameraFollow;
        [SerializeField] private GameObject environmentGo;
        [SerializeField] private LevelObjectsSpawnController levelObjectsSpawnController;


        [SerializeField] private Transform windowRoot;
        [SerializeField] private Transform nonActiveParent;

        private readonly ReactiveProperty<GameEndType> _gameEndStateType = new();
        private Camera _mainCamera;
        private PlayerController _playerController;
        private IWindowManager _windowManager;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _windowManager = new WindowManager(windowRoot, nonActiveParent);
        }

        private void Start()
        {
            _windowManager.GetWindow<MainMenuPanel>().Init(_windowManager, this);
            _windowManager.Open<MainMenuPanel>();
            levelObjectsSpawnController.SpawnAllLevelObjectsInFrustrum();
        }

        public IReadOnlyReactiveProperty<GameEndType> GameEndStateType => _gameEndStateType;

        public void StartGame()
        {
            levelManager.RestartLevel();

            _playerController = levelManager.CreatePlayer(playerGo, cameraFollow);
            _playerController.Init(_windowManager, this);
            _playerController.Wallet.MoneyCount.Subscribe(HandleMoneyValueChange);

            _windowManager.GetWindow<PlayerPanel>().Init(_windowManager,
                _playerController.GetComponentInChildren<PlayerTag>(), _mainCamera);
            _windowManager.Open<PlayerPanel>();

            environmentGo.SetActive(false);
            _windowManager.Close<MainMenuPanel>();
            InitPanels();
        }

        public void GameOver(GameEndType gameEndType)
        {
            SetEndLevelStateForPlayer();
            switch (gameEndType)
            {
                case GameEndType.Loosed:
                    Loose();
                    break;
                case GameEndType.Won:
                    Win();
                    break;
            }
        }

        private void InitPanels()
        {
            _windowManager.CloseSelected(new[]
            {
                typeof(MainMenuPanel),
                typeof(WinPanel),
                typeof(LoosePanel)
            });

            SubscribeOnEvents();
        }

        private void SubscribeOnEvents()
        {
            _windowManager.GetWindow<LoosePanel>().Init(Restart, Exit, _windowManager);
            _windowManager.GetWindow<WinPanel>().Init(Continue, Exit, _windowManager);
        }

        private void HandleMoneyValueChange(int value)
        {
            if (value == 0) GameOver(GameEndType.Loosed);
        }

        private void Continue()
        {
            _windowManager.Close<WinPanel>();
            levelManager.NextLevel();
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
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
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