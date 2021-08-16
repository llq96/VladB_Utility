using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VladB.Utility;


namespace VladB.GameTemplate {
    [RequireComponent(typeof(LevelsController_PlayerPrefs))]
    public class LevelsController : MonoBehaviour, IController {
        MainController mainController;

        [Header("Levels")]
        [SerializeField] string levelsPath;
        [SerializeField] public int countLevels;

        LevelsController_PlayerPrefs __playerPrefs;
        LevelsController_PlayerPrefs playerPrefs {
            get {
                if (__playerPrefs == null) {
                    __playerPrefs = GetComponent<LevelsController_PlayerPrefs>();
                }
                return __playerPrefs;

            }
        }

        [Header("Debug")]
        [Tooltip("If there is a level on the scene, then it will be launched.")]
        [SerializeField] bool isPlayLevelOnSceneIfExist = true;

        GameObject levelPrefab;
        Level level;

        #region Variables/Events Definitions

        #region PlayerPrefs
        public virtual int currentLevel {
            get { return playerPrefs.currentLevel; }
            set { playerPrefs.currentLevel = value; }
        }
        public virtual int loadedLevel {
            get { return playerPrefs.loadedLevel; }
            set { playerPrefs.loadedLevel = value; }
        }
        public virtual int lastPassedLevel {
            get { return playerPrefs.lastPassedLevel; }
            set { playerPrefs.lastPassedLevel = value; }
        }
        #endregion

        #region Events
        public delegate void LevelLoadedEv(Level _level);
        public event LevelLoadedEv OnLevelLoadedEv;
        #endregion

        #endregion


        #region IController Realization
        public virtual void Init(IMainController _mainController) {
            mainController = _mainController as MainController;
            currentLevel = lastPassedLevel + 1;//TODO ? После перезагрузки загружает уровень после последнего пройденного уровня, сделать enum 
        }

        public virtual void GameStateChanged(GameStateEnum _state) {
            switch (_state) {
                case GameStateEnum.Start:
                    if (level) {
                        level.gameObject.SetActive(false);
                    }
                    break;
                case GameStateEnum.Game:
                    if (level) {
                        level.gameObject.SetActive(true);
                    }
                    break;
            }
        }

        public virtual void LevelLoaded(Level _level) { }
        #endregion

        #region Load/Unload Level
        protected virtual void LoadLevel() {
            levelPrefab = null;

            if (isPlayLevelOnSceneIfExist) {
                isPlayLevelOnSceneIfExist = false;
                levelPrefab = FindLevelOnScene();
            }

            if (levelPrefab) {
                loadedLevel = -1;
            } else {
                loadedLevel = CalculatedLoadedLevel();
                levelPrefab = ResourcesLoader.LoadGameObject(levelsPath + loadedLevel);
            }

            level = Instantiate(levelPrefab).GetComponent<Level>();
            level.gameObject.SetActive(true);

            OnLevelLoadedEv?.Invoke(level);
        }

        protected virtual int CalculatedLoadedLevel() {
            int result = -1;
            if (currentLevel <= countLevels) {
                result = currentLevel;
            } else {
                result = (currentLevel % countLevels) + 1;
                //Убрать если уровни будут повторятся бесконечно
                //Debug.LogError("Wrong Level !!!");
                //return;
                //if (loadedLevel < 0) {
                //    loadedLevel = Random.Range(1, countLevels);
                //}
            }
            return result;
        }

        protected virtual GameObject FindLevelOnScene() {
            Level tempLevel = FindObjectOfType<Level>();
            if (tempLevel) {
                tempLevel.gameObject.SetActive(false);
                return tempLevel.gameObject;
            }
            return null;
        }



        protected virtual void UnLoadCurrentLevel() {
            if (level) {
                Destroy(level.gameObject);
            }
        }
        #endregion

        #region Other Functions

        public virtual void ReloadLevelWithGameStates(bool _isGameToo = true) {
            StartCoroutine(ReloadLevelWithGameStates_Cor(_isGameToo));
        }

        protected virtual IEnumerator ReloadLevelWithGameStates_Cor(bool _isGameToo = true) {
            //UNloading
            mainController.SetGameState(GameStateEnum.BeginUnloadingLevel);
            yield return WaitForAllReadyForUnLoad();
            UnLoadCurrentLevel();
            mainController.SetGameState(GameStateEnum.EndLoadLevel);

            //Loading
            mainController.SetGameState(GameStateEnum.StartLoadLevel);
            LoadLevel();
            mainController.SetGameState(GameStateEnum.EndLoadLevel);

            mainController.SetGameState(GameStateEnum.Start);

            if (_isGameToo) {
                mainController.SetGameState(GameStateEnum.Game);
            }
        }

        protected virtual IEnumerator WaitForAllReadyForUnLoad() {
            yield return new WaitForEndOfFrame();
        }

        public virtual void LevelCompleted() {
            lastPassedLevel = currentLevel;

            currentLevel++;
            //if (currentLevel >= countLevels) {
            //    loadedLevel = Random.Range(1, countLevels);
            //}

            levelPrefab = null;
        }
        #endregion
    }


}
