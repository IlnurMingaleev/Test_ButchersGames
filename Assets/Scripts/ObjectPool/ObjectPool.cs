﻿using System.Collections.Generic;
using PickUps;
using Unity.VisualScripting;
using UnityEngine;

namespace ObjectPool
{
    public class ObjectPool
    {
        private ILevelObjectFactory _levelObjectFactory;
        private Queue<GameObject> _bottlesPool;
        private Queue<GameObject> _moneyPool;

        public ObjectPool(ConfigManager configManager, List<Transform> _moneyTransforms,
            List<Transform> _bottleTransforms)
        {
            _bottlesPool = new Queue<GameObject>();
            _moneyPool = new Queue<GameObject>();
            _levelObjectFactory = new LevelObjectFactory(configManager, _moneyTransforms, _bottleTransforms);
        }

        public LevelObject GetNextLevelObject(LevelObjectType levelObjectType)
        {
            Queue<GameObject> queue;
            switch (levelObjectType)
            {
                case LevelObjectType.Money: return GetPooledObjectByType(levelObjectType, _moneyPool);
                case LevelObjectType.Bottle: return GetPooledObjectByType(levelObjectType, _bottlesPool);
            }

            return null;
        }

        private LevelObject GetPooledObjectByType(LevelObjectType levelObjectType, Queue<GameObject> queue)
        {
            if(queue.Count == 0) queue.Enqueue(_levelObjectFactory.CreateLevelObject(levelObjectType)); 
            LevelObject result = queue.Dequeue().GetComponent<LevelObject>();
            result.gameObject.SetActive(true);
            return result;
        }

        public void ReturnObjectToPool(GameObject gameObject, LevelObjectType levelObjectType)
        {
            gameObject.SetActive(false);
            gameObject.transform.SetParent(_levelObjectFactory.GetNextTransform(levelObjectType));
            gameObject.transform.localPosition = Vector3.zero;
            switch (levelObjectType)
            {
                case LevelObjectType.Money: EnquePooledObjectByType(gameObject, _moneyPool); break;
                case LevelObjectType.Bottle: EnquePooledObjectByType(gameObject, _bottlesPool); break;
            }
            
        }

        private void EnquePooledObjectByType(GameObject gameObject, Queue<GameObject> queue)
        {
            queue.Enqueue(gameObject);
        }
    }

    
}