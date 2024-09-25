using System;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Infrustructure
{
    public class LoosePanelBase:PanelBase
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _restartButton;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();

        public event Action OnRestartEvent;

        public event Action OnExitEvent;

        private void Start()
        {
            _exitButton.onClick.AsObservable().Subscribe(_=>Exit()).AddTo(_compositeDisposable);
            _restartButton.onClick.AsObservable().Subscribe(_=>Restart()).AddTo(_compositeDisposable);
        }

        public void Restart()
        {
            OnRestartEvent?.Invoke();
        }

        public void Exit()
        {
            OnExitEvent?.Invoke();
        }



    }
}