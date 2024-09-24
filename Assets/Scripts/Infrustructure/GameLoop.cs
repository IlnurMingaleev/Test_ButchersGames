using System;
using ButchersGames;
using UnityEngine;

namespace Infrustructure
{
    public class GameLoop:MonoBehaviour
    {
        [SerializeField] private LevelManager _levelManager;

        private void Start()
        {
            _levelManager.Init();
        }
    }
}