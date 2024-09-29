using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Runner.UI
{
    public interface IWindowManager
    {
        RectTransform WindowRoot { get; }
        T GetWindow<T>() where T : Window;
        T Open<T>() where T : Window;
        void Close<T>();
        void Remove<T>() where T : Window;
        void CloseAll();
        void CloseSelected(Type[] windowTypes);
    }

    public class WindowManager : IWindowManager
    {
        private readonly Transform _nonActiveParent;
        private readonly Transform _windowRoot;
        private readonly Dictionary<Type, Window> _windows;
        private Window _currentWindow;
        private Window _lastWindow;

        public WindowManager(Transform windowRoot, Transform nonActiveParent)
        {
            _windows = new Dictionary<Type, Window>();
            _windowRoot = windowRoot;
            _nonActiveParent = nonActiveParent;
        }

        public RectTransform WindowRoot => _windowRoot.GetComponent<RectTransform>();

        public T GetWindow<T>() where T : Window
        {
            T window;
            if (!_windows.ContainsKey(typeof(T))) CreateWindow<T>();
            window = (T) _windows[typeof(T)];

            return window;
        }

        public T Open<T>() where T : Window
        {
            T window;
            if (!_windows.ContainsKey(typeof(T))) CreateWindow<T>();

            window = (T) _windows[typeof(T)];
            window.GetComponent<RectTransform>().SetParent(_windowRoot);
            return window;
        }


        public void Close<T>()
        {
            if (_windows.ContainsKey(typeof(T)))
                _windows[typeof(T)].Close(_nonActiveParent);
            else
                Debug.LogError($"There is no window type called {typeof(T)}");
        }

        public void CloseAll()
        {
            foreach (var type in _windows.Keys)
                if (_windows.ContainsKey(type) && _windows[type].gameObject.activeInHierarchy)
                    _windows[type].Close(_nonActiveParent);
        }

        public void CloseSelected(Type[] windowTypes)
        {
            foreach (var type in windowTypes)
                if (_windows.ContainsKey(type) && _windows[type].gameObject.activeInHierarchy)
                    _windows[type].Close(_nonActiveParent);
        }

        public void Remove<T>() where T : Window
        {
            if (_windows.ContainsKey(typeof(T)))
            {
                var window = (T) _windows[typeof(T)];
                window.Close(_nonActiveParent);
                Object.Destroy(window.gameObject);
                _windows.Remove(typeof(T));
            }
        }

        private void CreateWindow<T>() where T : Window
        {
            T window;
            try
            {
                var windowGO =
                    (GameObject) Object.Instantiate(Resources.Load($"Windows/{typeof(T).Name}"),
                        _nonActiveParent);
                window = windowGO.GetComponent<T>();
                _windows.Add(typeof(T), window);
            }
            catch (Exception e)
            {
                Debug.LogError($"Couldn't retrieve window type {typeof(T)} from Resorces folder");
            }
        }
    }
}