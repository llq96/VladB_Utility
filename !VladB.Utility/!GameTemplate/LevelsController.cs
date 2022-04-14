using System.Collections;
using UnityEngine;
using VladB.Utility;


namespace VladB.GameTemplate {
    [RequireComponent(typeof(LevelsController_PlayerPrefs))]
    public class LevelsController : MonoBehaviour, IController {
        MainController mainController;

        [Header("Levels")]
        [SerializeField] protected string levelsPath;
        [SerializeField] protected int countLevels;

        [Header("Debug")]
        [Tooltip("If there is a level on the scene, then it will be launched.")]
        [SerializeField] protected bool isPlayLevelOnSceneIfExist = true;

        protected LevelsController_PlayerPrefs __playerPrefs;
        protected LevelsController_PlayerPrefs playerPrefs {
            get {
                if(__playerPrefs == null) {
                    __playerPrefs = GetComponent<LevelsController_PlayerPrefs>();
                }
                return __playerPrefs;

            }
        }

        protected GameObject levelPrefab;
        protected Level level;

        #region Variables PlayerPrefs
        public virtual int currentLevel {
            get => playerPrefs.currentLevel;
            set => playerPrefs.currentLevel = value;
        }
        public virtual int loadedLevel {
            get => playerPrefs.loadedLevel;
            set => playerPrefs.loadedLevel = value;
        }
        public virtual int lastPassedLevel {
            get => playerPrefs.lastPassedLevel;
            set => playerPrefs.lastPassedLevel = value;
        }
        #endregion


        #region IController Realization
        public virtual void Init(MainController mainController) {
            this.mainController = mainController;
            currentLevel = lastPassedLevel + 1;
        }

        public virtual void GameStateChanged(GameStateEnum state, params object[] parameters) {
            switch(state) {
                case GameStateEnum.Start:
                    if(level) {
                        level.gameObject.SetActive(false);
                    }
                    break;
                case GameStateEnum.Game:
                    if(level) {
                        level.gameObject.SetActive(true);
                    }
                    break;
            }
        }
        #endregion

        #region Load/Unload Level
        protected virtual void LoadLevel() {
            levelPrefab = null;

            if(isPlayLevelOnSceneIfExist) {
                isPlayLevelOnSceneIfExist = false;
                levelPrefab = FindLevelOnScene();
            }

            if(levelPrefab) {
                loadedLevel = -1;
            } else {
                loadedLevel = CalculatedLoadedLevel();
                levelPrefab = ResourcesLoader.LoadGameObject(levelsPath + loadedLevel);
            }

            level = Instantiate(levelPrefab).GetComponent<Level>();
        }

        protected virtual int CalculatedLoadedLevel() {
            //TODO после того как уровни закончатся:
            //рандомный уровень после прохождения всех
            //Минимальный уровень
            int result = -1;
            if(currentLevel <= countLevels) {
                result = currentLevel;
            } else {
                result = ((currentLevel - 1) % countLevels) + 1;
            }
            return result;
        }

        protected virtual GameObject FindLevelOnScene() {
            Level tempLevel = FindObjectOfType<Level>();
            if(tempLevel) {
                tempLevel.gameObject.SetActive(false);
                return tempLevel.gameObject;
            }
            return null;
        }



        protected virtual void UnLoadCurrentLevel() {
            if(level) {
                Destroy(level.gameObject);
            }
        }
        #endregion

        #region Other Functions
        public virtual void ReloadLevelWithGameStates(bool isGameToo = true) {
            StartCoroutine(ReloadLevelWithGameStates_Cor(isGameToo));
        }

        protected virtual IEnumerator ReloadLevelWithGameStates_Cor(bool isGameToo = true) {
            yield return UnLoading_Cor();
            yield return Loading_Cor();

            //Next GameStates
            mainController.SetGameState(GameStateEnum.Start);

            if(isGameToo) {
                mainController.SetGameState(GameStateEnum.Game);
            }
        }

        protected virtual IEnumerator UnLoading_Cor() {
            mainController.SetGameState(GameStateEnum.BeginUnloadingLevel);
            UnLoadCurrentLevel();
            mainController.SetGameState(GameStateEnum.EndUnloadingLevel);
            yield break;
        }

        protected virtual IEnumerator Loading_Cor() {
            mainController.SetGameState(GameStateEnum.BeginLoadLevel);
            LoadLevel();
            mainController.SetGameState(GameStateEnum.LevelLoaded, level);
            level.Init(mainController);
            mainController.SetGameState(GameStateEnum.LevelInitialized, level);
            yield break;
        }



        public virtual void LevelCompleted() {
            lastPassedLevel = currentLevel;
            currentLevel++;
            levelPrefab = null;
        }
        #endregion
    }
}
