using System;
using UniRx;
using UnityEngine;

namespace UI
{
    public abstract class Window:MonoBehaviour,IDisposable
    {
        protected IWindowManager _manager;
        protected CompositeDisposable _disposable = new CompositeDisposable();
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

        public void Dispose()
        {
            _disposable?.Clear();
        }
    }
}