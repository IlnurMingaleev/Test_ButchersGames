using System;
using Infrustructure;
using PathCreation.Examples;
using UI;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    

    public class PlayerController : MonoBehaviour
    {
        public PlayerAnimator Animator { get; private set; }
        public PathFollower PathFollower { get; private set; }
        public PlayerMovement Movement { get; private set; }
        public IWallet Wallet { get; private set; }
        
        public IGameStateChangeable GameStateChanger {get; private set; }

        private IWindowManager _windowManager;
        public event Action<GameEndType> EndGameEvent;


        private void Awake()
        {
            Animator = GetComponent<PlayerAnimator>();
            PathFollower = GetComponent<PathFollower>();
            Movement = GetComponent<PlayerMovement>();
        }

        public void Init(IWindowManager windowManager, IGameStateChangeable gameStateChangeable)
        {
            _windowManager= windowManager;
            Wallet = new PlayerWallet(_windowManager);
            GameStateChanger = gameStateChangeable;
        }
        

    }
}