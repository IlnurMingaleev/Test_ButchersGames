using System.Collections.Generic;
using Runner.SO;
using UnityEngine;

namespace Runner
{
    public interface ILevelObjectFactory
    {
        GameObject CreateLevelObject(LevelObjectType levelObjectType);
        Transform GetNextTransform(LevelObjectType levelObjectType);

        void IncrementCurrentLevelObjectIndex(LevelObjectType levelObjectType);
    }

    public class LevelObjectFactory : ILevelObjectFactory
    {
        private readonly ConfigManager _configManager;

        private readonly Dictionary<LevelObjectType, (int, List<Transform>)> _leveObjectSceneTransforms;

        //private Dictionary<LevelObjectType, int> _levelObjectsLastIndexes;
        public LevelObjectFactory(ConfigManager configManager, List<Transform> _moneyTransforms,
            List<Transform> _bottleTransforms)
        {
            _leveObjectSceneTransforms = new Dictionary<LevelObjectType, (int, List<Transform>)>
            {
                {LevelObjectType.Money, (0, _moneyTransforms)},
                {LevelObjectType.Bottle, (0, _bottleTransforms)}
            };
            /*_levelObjectsLastIndexes = new Dictionary<LevelObjectType, int>()
            {
                {LevelObjectType.Money, 0},
                {LevelObjectType.Bottle, 0},
            };*/
            _configManager = configManager;
        }

        public GameObject CreateLevelObject(LevelObjectType levelObjectType)
        {
            
            var levelObject = Object.Instantiate(
                _configManager.LevelObjectsSO.LevelObjectsByType[levelObjectType].gameObject,
                GetNextTransform(levelObjectType));
            if (!levelObject.activeSelf) levelObject.SetActive(true);
            return levelObject;
        }

        public Transform GetNextTransform(LevelObjectType levelObjectType)
        {
            var levelObjectsTransfroms = _leveObjectSceneTransforms[levelObjectType];
            var lastTransform = levelObjectsTransfroms.Item2[levelObjectsTransfroms.Item1];
            return lastTransform;
        }

        public void IncrementCurrentLevelObjectIndex(LevelObjectType levelObjectType)
        {
            var levelObjectsTransfroms = _leveObjectSceneTransforms[levelObjectType];
            _leveObjectSceneTransforms[levelObjectType] =
                (++levelObjectsTransfroms.Item1, levelObjectsTransfroms.Item2);
        }
    }
}