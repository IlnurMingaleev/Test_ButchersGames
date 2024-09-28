using System;
using System.Collections.Generic;
using Data;
using ObjectPool;
using PickUps;
using UnityEngine;

namespace Infrustructure
{
    public class LevelObjectsSpawnController:MonoBehaviour
    {
        [SerializeField] private List<LevelObjectTag> _levelObjects = new List<LevelObjectTag>();
        [SerializeField] private ConfigManager _configManager;
        private List<Transform> _moneyTransforms;
        private List<Transform> _bottleTransforms;
        private Camera _mainCamera;
        private float _spawnDistance;
        private float _outOfViewDistance;   
        private int _currentSpawnIndex;
        private ObjectPool.ObjectPool _levelObjectPool;

        private Queue<LevelObject> _sceneLevelObjects;

        private void Awake()
        {
            _moneyTransforms = new List<Transform>();
            _bottleTransforms = new List<Transform>();
            _sceneLevelObjects = new Queue<LevelObject>();
            _mainCamera = Camera.main;
            _currentSpawnIndex = 0;
            if (_mainCamera != null)
            {
                _spawnDistance = _mainCamera.farClipPlane;
                _outOfViewDistance = 0;
            }
            InitTransformsByType();
            _levelObjectPool = new ObjectPool.ObjectPool(_configManager, _moneyTransforms, _bottleTransforms);
            
        }

        public void SpawnAllLevelObjectsInFrustrum()
        {
            while (IsLevelObjectInSpawnDistance())
            {
                _sceneLevelObjects.Enqueue(_levelObjectPool.GetNextLevelObject(_levelObjects[_currentSpawnIndex].Type));
                _currentSpawnIndex++;
            }
        }

        private void FixedUpdate()
        {
            ReturnOutOfViewLevelObjects();
            SpawnAllLevelObjectsInFrustrum();
            
        }

        private void ReturnOutOfViewLevelObjects()
        {
            if(_sceneLevelObjects.Count == 0) return;
            else
            {
                LevelObject checkedLevelObject = _sceneLevelObjects.Peek();
                while (IsLevelObjectOutOfViewDistance(checkedLevelObject.gameObject))
                {
                    _levelObjectPool.ReturnObjectToPool(checkedLevelObject.gameObject, checkedLevelObject.LevelObjectType);
                    _sceneLevelObjects.Dequeue();
                    ReturnOutOfViewLevelObjects();
                }
            }
            
            
        }

        private bool IsLevelObjectInSpawnDistance()
        {
            Transform levelObjectTransform = _levelObjects[_currentSpawnIndex].transform;
            return Vector3.Distance(levelObjectTransform.position, _mainCamera.transform.position) <= _spawnDistance;
        }

        private bool IsLevelObjectOutOfViewDistance(GameObject levelObject)
        {
            Vector3 lastLevelObjectDir = levelObject.transform.position - _mainCamera.transform.position;
            float angle = Vector3.SignedAngle(lastLevelObjectDir,_mainCamera.transform.forward,Vector3.up);
            if (angle > -90 && angle < 90) return false;
            else return true;
        }
        

        private void InitTransformsByType()
        {
            for(int index =0; index < _levelObjects.Count; index++)
            {
                var levelObjectModel = _levelObjects[index];
                switch (levelObjectModel.Type)
                {
                    case LevelObjectType.Money: _moneyTransforms.Add(levelObjectModel.transform); break;
                    case LevelObjectType.Bottle:_bottleTransforms.Add(levelObjectModel.transform); break;
                }
            }
        }
        

    }
    
}