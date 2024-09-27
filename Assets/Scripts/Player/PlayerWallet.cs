using UI;
using UniRx;
using UnityEngine;

namespace Player
{
    public interface IWallet
    {
        void AddMoney(int amount);
        IReadOnlyReactiveProperty<int> MoneyCount { get; }
    }
    public class PlayerWallet:IWallet
    {
        private const int MAX_MONEY_COUNT = 50;
        private const int START_MONEY_AMOUNT = 20;
        private IWindowManager _windowManager;
        private ReactiveProperty<int> _moneyCount;
        public IReadOnlyReactiveProperty<int> MoneyCount => _moneyCount;

        public PlayerWallet(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            _moneyCount= new ReactiveProperty<int>(START_MONEY_AMOUNT);
            _moneyCount.Subscribe((_) => _windowManager.GetWindow<PlayerPanel>().UpdateSlider(_, MAX_MONEY_COUNT));
        }
        public void AddMoney(int amount)
        {
            _moneyCount.Value= ValidateMoneyCount(_moneyCount.Value +amount);
        }
        public void MinusMoneyCount(int amount)
        {
            _moneyCount.Value = ValidateMoneyCount(_moneyCount.Value - amount);
        }
        
        private int ValidateMoneyCount(int value)
        {
            return Mathf.Clamp(value,0,MAX_MONEY_COUNT);
        }
    }
}