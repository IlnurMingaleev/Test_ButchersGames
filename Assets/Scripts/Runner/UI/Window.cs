using System;
using UniRx;
using UnityEngine;

namespace Runner.UI
{
    public abstract class Window : MonoBehaviour, IDisposable
    {
        protected CompositeDisposable _disposable = new();
        protected IWindowManager _manager;

        public void Dispose()
        {
            _disposable?.Clear();
        }

        public virtual void Init(IWindowManager _windowManager)
        {
            _manager = _windowManager;
        }

        public void Open(Transform activeParent)
        {
            transform.SetParent(activeParent);
        }

        public void Close(Transform nonActiveParent)
        {
            transform.SetParent(nonActiveParent);
            _disposable?.Clear();
        }
    }
}