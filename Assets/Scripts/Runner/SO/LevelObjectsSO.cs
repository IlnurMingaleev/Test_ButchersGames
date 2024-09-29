using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Runner.SO
{
    [CreateAssetMenu(fileName = "LevelObjectSO", menuName = "ConfigSO")]
    public class LevelObjectsSO : ScriptableObject
    {
        [SerializeField] private List<LevelObject> _levelObjects;
        private ReactiveDictionary<LevelObjectType, LevelObject> _levelObjectsByType;
        public IReadOnlyReactiveDictionary<LevelObjectType, LevelObject> LevelObjectsByType => _levelObjectsByType;

        public void Init()
        {
            _levelObjectsByType.Clear();
            foreach (var levelObject in _levelObjects)
                _levelObjectsByType.Add(levelObject.LevelObjectType, levelObject);
        }
    }
}