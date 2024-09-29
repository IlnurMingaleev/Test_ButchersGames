using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Runner.UI
{
    public class MainMenuPanel : Window
    {
        [SerializeField] private Image _palmImage;
        [SerializeField] private Vector2 _minPosition;
        [SerializeField] private Vector2 _maxPosition;
        [SerializeField] private Button _playButton;
        private readonly float _delayBtwMoves = 1f;
        private readonly float _duration = 2;

        private IGameStateChangeable _gameStateChangeable;

        private void Start()
        {
            _playButton.onClick.AsObservable().Subscribe(_ => { _gameStateChangeable.StartGame(); }).AddTo(_disposable);
            MoveToStartPoint();
        }

        public void Init(IWindowManager windowManager, IGameStateChangeable gameStateChangeable)
        {
            base.Init(windowManager);
            _gameStateChangeable = gameStateChangeable;
        }

        private void MoveToStartPoint()
        {
            _palmImage.rectTransform.DOAnchorPos(_minPosition, _duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                Invoke(nameof(MoveToEndPoint), _delayBtwMoves);
            });
        }

        private void MoveToEndPoint()
        {
            _palmImage.rectTransform.DOAnchorPos(_maxPosition, _duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                Invoke(nameof(MoveToStartPoint), _delayBtwMoves);
            });
        }
    }
}