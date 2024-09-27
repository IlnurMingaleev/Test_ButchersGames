using System;
using System.Collections;
using DG.Tweening;
using Infrustructure;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuPanel:Window
    {
        [SerializeField] private Image _palmImage;
        [SerializeField] private Vector2 _minPosition;
        [SerializeField] private Vector2 _maxPosition;
        [SerializeField] private Button _playButton;
        private float _duration = 2;
        private float _delayBtwMoves = 1f;

        private IGameStateChangeable _gameStateChangeable;

        public void Init(IWindowManager windowManager, IGameStateChangeable gameStateChangeable)
        {
            base.Init(windowManager);
            _gameStateChangeable = gameStateChangeable;
        }

        private void Start()
        {
            _playButton.onClick.AsObservable().Subscribe((_) =>
            {
                _gameStateChangeable.StartGame();
            }).AddTo(_disposable);
            MoveToStartPoint();
        }

        void MoveToStartPoint()
        {
            _palmImage.rectTransform.DOAnchorPos(_minPosition, _duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                Invoke(nameof(MoveToEndPoint), _delayBtwMoves);
            });
        }
        
        void MoveToEndPoint()
        {
            _palmImage.rectTransform.DOAnchorPos(_maxPosition, _duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                Invoke(nameof(MoveToStartPoint), _delayBtwMoves);
            });
        }

       
    }
}