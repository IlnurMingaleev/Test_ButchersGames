using SO;
using UnityEngine;

namespace ObjectPool
{
    public class ConfigManager:MonoBehaviour
    {
        [SerializeField] private LevelObjectsSO _levelObjectsSO;
        public LevelObjectsSO LevelObjectsSO => _levelObjectsSO;
        private void Awake()
        {
            _levelObjectsSO.Init();
        }
    }
}