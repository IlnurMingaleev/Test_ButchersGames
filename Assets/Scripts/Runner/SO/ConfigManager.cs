using UnityEngine;

namespace Runner.SO
{
    public class ConfigManager : MonoBehaviour
    {
        [SerializeField] private LevelObjectsSO _levelObjectsSO;
        public LevelObjectsSO LevelObjectsSO => _levelObjectsSO;

        private void Awake()
        {
            _levelObjectsSO.Init();
        }
    }
}