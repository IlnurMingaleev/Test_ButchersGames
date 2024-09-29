using System.Collections.Generic;
using Runner.SO;
using UnityEngine;

namespace Runner
{
    public class LevelObjectsSpawnController : MonoBehaviour
    {
        [SerializeField] private List<LevelObjectTag> levelObjects = new();
        [SerializeField] private ConfigManager configManager;
        private List<Transform> _bottleTransforms;
        private int _currentSpawnIndex;
        private ObjectPool _levelObjectPool;
        private Camera _mainCamera;
        private List<Transform> _moneyTransforms;
        private float _outOfViewDistance;

        private Queue<LevelObject> _sceneLevelObjects;
        private float _spawnDistance;

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
            _levelObjectPool = new ObjectPool(configManager, _moneyTransforms, _bottleTransforms);
        }

        private void FixedUpdate()
        {
            ReturnOutOfViewLevelObjects();
            SpawnAllLevelObjectsInFrustrum();
        }

        public void SpawnAllLevelObjectsInFrustrum()
        {
            while (IsLevelObjectInSpawnDistance())
            {
                _sceneLevelObjects.Enqueue(_levelObjectPool.GetNextLevelObject(levelObjects[_currentSpawnIndex].Type));
                _currentSpawnIndex++;
            }
        }

        private void ReturnOutOfViewLevelObjects()
        {
            if (_sceneLevelObjects.Count == 0) return;

            var checkedLevelObject = _sceneLevelObjects.Peek();
            if(IsLevelObjectOutOfViewDistance(checkedLevelObject.gameObject))
            {
                _levelObjectPool.ReturnObjectToPool(checkedLevelObject.gameObject, checkedLevelObject.LevelObjectType);
                _sceneLevelObjects.Dequeue();
                ReturnOutOfViewLevelObjects();
            }
            
        }

        private bool IsLevelObjectInSpawnDistance()
        {
            var levelObjectTransform = levelObjects[_currentSpawnIndex].transform;
            return Vector3.Distance(levelObjectTransform.position, _mainCamera.transform.position) <= _spawnDistance;
        }

        private bool IsLevelObjectOutOfViewDistance(GameObject levelObject)
        {
            var lastLevelObjectDir = levelObject.transform.position - _mainCamera.transform.position;
            var angle = Vector3.SignedAngle(lastLevelObjectDir, _mainCamera.transform.forward, Vector3.up);
            if (angle > -90 && angle < 90) return false;
            return true;
        }


        private void InitTransformsByType()
        {
            for (var index = 0; index < levelObjects.Count; index++)
            {
                var levelObjectModel = levelObjects[index];
                switch (levelObjectModel.Type)
                {
                    case LevelObjectType.Money:
                        _moneyTransforms.Add(levelObjectModel.transform);
                        break;
                    case LevelObjectType.Bottle:
                        _bottleTransforms.Add(levelObjectModel.transform);
                        break;
                }
            }
        }
    }
}