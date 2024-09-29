using System;
using System.Collections.Generic;
using System.Linq;
using Runner.Player;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runner
{
    public class LevelManager : MonoBehaviour
    {
        private const string CurrentLevel_PrefsKey = "Current Level";
        private const string CompleteLevelCount_PrefsKey = "Complete Lvl Count";
        private const string LastLevelIndex_PrefsKey = "Last Level Index";
        private const string CurrentAttempt_PrefsKey = "Current Attempt";
        public int CurrentLevelIndex;

        [SerializeField] private bool editorMode;
        [SerializeField] private LevelsList levels;

        public Level CurrentLevelInstance;

        public GameObject CurrentPlayer;

        public static int CurrentLevel
        {
            get => (CompleteLevelCount < Default.Levels.Count ? Default.CurrentLevelIndex : CompleteLevelCount) + 1;
            set => PlayerPrefs.GetInt(CurrentLevel_PrefsKey, value);
        }

        public static int CompleteLevelCount
        {
            get => PlayerPrefs.GetInt(CompleteLevelCount_PrefsKey);
            set => PlayerPrefs.SetInt(CompleteLevelCount_PrefsKey, value);
        }

        public static int LastLevelIndex
        {
            get => PlayerPrefs.GetInt(LastLevelIndex_PrefsKey);
            set => PlayerPrefs.SetInt(LastLevelIndex_PrefsKey, value);
        }

        public static int CurrentAttempt
        {
            get => PlayerPrefs.GetInt(CurrentAttempt_PrefsKey);
            set => PlayerPrefs.SetInt(CurrentAttempt_PrefsKey, value);
        }

        public List<Level> Levels => levels.lvls;

        private void OnDestroy()
        {
            LastLevelIndex = CurrentLevelIndex;
        }

        private void OnApplicationQuit()
        {
            LastLevelIndex = CurrentLevelIndex;
        }

        public event Action OnLevelStarted;

        public void Init()
        {
#if !UNITY_EDITOR
            editorMode = false;
#endif
            if (!editorMode) SelectLevel(LastLevelIndex);

            if (LastLevelIndex != CurrentLevel) CurrentAttempt = 0;
        }


        public void StartLevel()
        {
            OnLevelStarted?.Invoke();
        }

        public void RestartLevel()
        {
            SelectLevel(CurrentLevelIndex, false);
        }

        public void NextLevel()
        {
            if (!editorMode) CurrentLevel++;
            SelectLevel(CurrentLevelIndex + 1);
        }

        public void SelectLevel(int levelIndex, bool indexCheck = true)
        {
            if (indexCheck)
                levelIndex = GetCorrectedIndex(levelIndex);

            if (Levels[levelIndex] == null)
            {
                Debug.Log("<color=red>There is no prefab attached!</color>");
                return;
            }

            var level = Levels[levelIndex];

            if (level)
            {
                SelLevelParams(level);
                CurrentLevelIndex = levelIndex;
            }
        }

        public void PrevLevel()
        {
            SelectLevel(CurrentLevelIndex - 1);
        }

        private int GetCorrectedIndex(int levelIndex)
        {
            if (editorMode) return levelIndex > Levels.Count - 1 || levelIndex <= 0 ? 0 : levelIndex;

            var levelId = CurrentLevel;
            if (levelId > Levels.Count - 1)
            {
                if (levels.randomizedLvls)
                {
                    var lvls = Enumerable.Range(0, levels.lvls.Count).ToList();
                    lvls.RemoveAt(CurrentLevelIndex);
                    return lvls[Random.Range(0, lvls.Count)];
                }

                return levelIndex % levels.lvls.Count;
            }

            return levelId;
        }

        private void SelLevelParams(Level level)
        {
            if (level)
            {
                ClearChilds();
#if UNITY_EDITOR
                if (Application.isPlaying)
                {
                    Instantiate(level, transform).TryGetComponent(out CurrentLevelInstance);
                }
                else
                {
                    var currentLevelObject = PrefabUtility.InstantiatePrefab(level, transform);
                    ((GameObject) currentLevelObject).TryGetComponent(out CurrentLevelInstance);
                }
#else
                Instantiate(level, transform).TryGetComponent(out CurrentLevelInstance);

#endif
            }
        }

        private void ClearChilds()
        {
            for (var i = 0; i < transform.childCount; i++)
            {
                var destroyObject = transform.GetChild(i).gameObject;
                DestroyImmediate(destroyObject);
            }
        }

        public PlayerController CreatePlayer(GameObject playerPrefab, CameraFollow cameraFollow)
        {
            var playerGO = Instantiate(playerPrefab, CurrentLevelInstance.PlayerSpawnPoint);
            var playerController = playerGO.GetComponent<PlayerController>();
            cameraFollow.SetPlayerTransfrom(playerController.Movement.PlayertTransform);
            playerController.PathFollower.pathCreator = CurrentLevelInstance.PathCreator;
            CurrentPlayer = playerGO;
            return playerController;
        }

        #region Singletone

        public static LevelManager Default { get; private set; }

        public LevelManager()
        {
            Default = this;
        }

        #endregion
    }
}