using System.Collections.Generic;
using PickUps;
using UnityEngine;

namespace ObjectPool
{
    public interface ILevelObjectFactory
    {
        GameObject  CreateLevelObject(LevelObjectType levelObjectType);
        Transform GetNextTransform(LevelObjectType levelObjectType);
    }
    public class LevelObjectFactory : ILevelObjectFactory
    {
        private ConfigManager _configManager;
        private Dictionary<LevelObjectType, (int,List<Transform>)> _leveObjectSceneTransforms;
        //private Dictionary<LevelObjectType, int> _levelObjectsLastIndexes;
        public LevelObjectFactory(ConfigManager configManager, List<Transform> _moneyTransforms,
            List<Transform> _bottleTransforms)
        {
            _leveObjectSceneTransforms = new Dictionary<LevelObjectType,(int, List<Transform>)>()
            {
                { LevelObjectType.Money, (0,_moneyTransforms) },
                { LevelObjectType.Bottle, (0,_bottleTransforms) },
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
            (int, List<Transform>) levelObjectsTransfroms= _leveObjectSceneTransforms[levelObjectType];
            GameObject levelObject  = Object.Instantiate(
                _configManager.LevelObjectsSO.LevelObjectsByType[levelObjectType].gameObject,
               GetNextTransform(levelObjectType));
            _leveObjectSceneTransforms[levelObjectType] = (++levelObjectsTransfroms.Item1, levelObjectsTransfroms.Item2);
            if(!levelObject.activeSelf) levelObject.SetActive(true);
            return levelObject;
        }

        public Transform GetNextTransform(LevelObjectType levelObjectType)
        {
            (int, List<Transform>) levelObjectsTransfroms= _leveObjectSceneTransforms[levelObjectType];
            Transform lastTransform =  levelObjectsTransfroms.Item2[levelObjectsTransfroms.Item1];
            _leveObjectSceneTransforms[levelObjectType] = (++levelObjectsTransfroms.Item1, levelObjectsTransfroms.Item2);
            return lastTransform;
        }
    }
}