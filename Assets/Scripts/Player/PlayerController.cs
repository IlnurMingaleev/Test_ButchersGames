using System;
using DefaultNamespace;
using Enums;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public interface IWallet
    {
        void AddMoney(int amount);
        void MinusMoneyCount(int amount);
    }

    public class PlayerController : MonoBehaviour, IWallet
    {
        [SerializeField] private Slider _moneySlider;

        private ReactiveProperty<int> _moneyCount = new ReactiveProperty<int>(5);
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private float _maxMoneyValue = 50;
        public IReadOnlyReactiveProperty<int> MoneyCount => _moneyCount;

        public event Action<GameEndEnum> EndGameEvent;

        private void Start()
        {
            _moneyCount.ObserveEveryValueChanged(x => x).Subscribe(_=> DisplayUI()).AddTo(_compositeDisposable);
        }

        public void DisplayUI()
        {
            float tempValue = (_moneyCount.Value / _maxMoneyValue);
            _moneySlider.value = (tempValue <= 0)?0:tempValue;
            _moneySlider.value = (tempValue >= 1) ? 1 : tempValue;
        }

        public void SetMoneyCount(int moneyCount)
        {
            _moneyCount.Value = moneyCount;
            DisplayUI();
        }

        public void AddMoney(int amount)
        {
            int tempValue = _moneyCount.Value + amount;
            _moneyCount.Value= (tempValue >= 100) ? 100 : tempValue;
            Debug.Log(_moneyCount);
            DisplayUI();
        }

        public void MinusMoneyCount(int amount)
        {
            int tempValue = _moneyCount.Value - amount;
            _moneyCount.Value = (tempValue <= 0)?0:tempValue;
            if (_moneyCount.Value == 0) EndGameEvent?.Invoke(GameEndEnum.Loosed);
            Debug.Log(_moneyCount); 
            DisplayUI();
        }
        
    }
}