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
            currentLevel = lastPassedLevel + 1;//TODO ? ����� ������������ ��������� ������� ����� ���������� ����������� ������, ������� enum 
        }

        public virtual void GameStateChanged(GameStateEnum state, params object[] parameters) {
            switch(state) {
                case GameStateEnum.Start:
                    if(level) {
                        level.gameObject.SetActive(false); //���� ��?
                    }
                    break;
                case GameStateEnum.Game:
                    if(level) {
                        level.gameObject.SetActive(true);//���� �� ?
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

        protected virtual int CalculatedLoadedLevel() {//TODO
            int result = -1;
            if(currentLevel <= countLevels) {
                result = currentLevel;
            } else {
                result = (currentLevel % countLevels) + 1;
                //������ ���� ������ ����� ���������� ����������
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
            //UNloading
            mainController.SetGameState(GameStateEnum.BeginUnloadingLevel);
            yield return WaitForAllReadyForUnLoad();
            UnLoadCurrentLevel();
            mainController.SetGameState(GameStateEnum.EndUnloadingLevel);

            //Loading
            mainController.SetGameState(GameStateEnum.BeginLoadLevel);
            LoadLevel();
            level.gameObject.SetActive(true); //���� ��?
            level.Init(mainController);
            mainController.SetGameState(GameStateEnum.LevelLoaded, level);

            //Next GameStates
            mainController.SetGameState(GameStateEnum.Start);

            if(isGameToo) {
                mainController.SetGameState(GameStateEnum.Game);
            }
        }

        protected virtual IEnumerator WaitForAllReadyForUnLoad() {//TODO
            yield return new WaitForEndOfFrame();
        }

        public virtual void LevelCompleted() {
            lastPassedLevel = currentLevel;
            currentLevel++;
            levelPrefab = null;
        }
        #endregion
    }
}
