using System;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Infrustructure
{
    public class WinPanel:Window
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _continueButton;

        public void Init(Action OnContinue, Action OnExit, IWindowManager windowManager)
        {
            base.Init(windowManager);
            _exitButton.onClick.AsObservable().Subscribe(_ =>
            {
                OnExit?.Invoke();
            }).AddTo(_disposable);
            _continueButton.onClick.AsObservable().Subscribe(_=>
            {
                OnContinue?.Invoke();
            }).AddTo(_disposable);
        }
        
    }
}