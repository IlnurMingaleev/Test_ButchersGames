using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public interface IPanel
    {
        void Open();
        void Close();
    }

    public class MainMenuPanel:MonoBehaviour, IPanel
    {
        [SerializeField] private Image _palmImage;
        [SerializeField] private Vector2 _minPosition;
        [SerializeField] private Vector2 _maxPosition;
        private float _duration = 2;
        private float _delayBtwMoves = 1f;

        private void Start()
        {
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

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}