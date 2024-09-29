using UniRx;

namespace Runner
{
    public class GameEventService
    {
        private readonly ReactiveProperty<GameEndType> _gameEndStateType = new();
        private IGameStateChangeable _gameStateChangeable;

        public GameEventService(IGameStateChangeable gameStateChangeable)
        {
            _gameStateChangeable = gameStateChangeable;
        }

        public IReadOnlyReactiveProperty<GameEndType> GameEndStateType => _gameEndStateType;

        public void OnGameStateChanged(GameEndType gameEndType)
        {
            switch (gameEndType)
            {
                case GameEndType.Loosed:
                    break;
                case GameEndType.Won:
                    break;
            }
        }
    }
}