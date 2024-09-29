using System;
using PathCreation.Examples;
using Runner.UI;
using UnityEngine;

namespace Runner.Player
{
    public class PlayerController : MonoBehaviour
    {
        private IWindowManager _windowManager;
        public PlayerAnimator Animator { get; private set; }
        public PathFollower PathFollower { get; private set; }
        public PlayerMovement Movement { get; private set; }
        public IWallet Wallet { get; private set; }

        public IGameStateChangeable GameStateChanger { get; private set; }


        private void Awake()
        {
            Animator = GetComponent<PlayerAnimator>();
            PathFollower = GetComponent<PathFollower>();
            Movement = GetComponent<PlayerMovement>();
        }

        public event Action<GameEndType> EndGameEvent;

        public void Init(IWindowManager windowManager, IGameStateChangeable gameStateChangeable)
        {
            _windowManager = windowManager;
            Wallet = new PlayerWallet(_windowManager);
            GameStateChanger = gameStateChangeable;
        }
    }
}