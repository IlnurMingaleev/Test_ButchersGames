using UniRx;

namespace Infrustructure
{
    public class GameEventService
    {
        private IGameStateChangeable _gameStateChangeable;
        private ReactiveProperty<GameEndType> _gameEndStateType = new ReactiveProperty<GameEndType>();
        public IReadOnlyReactiveProperty<GameEndType> GameEndStateType => _gameEndStateType;

        public GameEventService(IGameStateChangeable gameStateChangeable)
        {
            _gameStateChangeable = gameStateChangeable;
        }

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