using System;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Infrustructure
{
    public class LoosePanel:Window
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _restartButton;
        public void Init(Action OnRestart, Action OnExit, IWindowManager windowManager)
        {
            
            base.Init(windowManager);
            _exitButton.onClick.AsObservable().Subscribe(_ =>
            {
                OnExit?.Invoke();
            }).AddTo(_disposable);
            _restartButton.onClick.AsObservable().Subscribe(_=>
            {
                OnRestart?.Invoke();
            }).AddTo(_disposable);
        }
        
    }
}