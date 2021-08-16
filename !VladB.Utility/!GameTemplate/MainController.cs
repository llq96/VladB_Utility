using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VladB.GameTemplate {
    [RequireComponent(typeof(GameState))]
    public class MainController : MonoBehaviour, IMainController {
        [HideInInspector] public GameState gameState;

        [Header("Objects With IController")]
        [SerializeField] List<GameObject> controllersObjects = new List<GameObject>();
        List<IController> controllers = new List<IController>();

        [Header("Debug")]
        [SerializeField] bool isDebugLog;

        public virtual void Init() {
            gameState = GetComponent<GameState>();
            gameState.OnGameStateChanged -= GameStateChanged;
            //...
            gameState.OnGameStateChanged += GameStateChanged;

            IController temp;
            foreach (GameObject go in controllersObjects) {
                if (go) {
                    temp = go.GetComponent<IController>();
                    if (temp != null) {
                        controllers.Add(temp);
                    } else {
                        if (isDebugLog) {
                            Debug.LogError($"{go.name} doesn`t have IController !");
                        }
                    }
                } else {
                    if (isDebugLog) {
                        Debug.LogError("GameObjet is null! ");
                    }
                }
            }

            foreach (IController controller in controllers) {
                if (controller != null) {
                    controller.Init(this);
                    gameState.OnGameStateChanged -= controller.GameStateChanged;
                    gameState.OnGameStateChanged += controller.GameStateChanged;
                    if (isDebugLog) {
                        Debug.Log($"Init {controller}");
                    }
                } else {
                    if (isDebugLog) {
                        Debug.LogError("controller is null !");
                    }
                }
            }
        }

        public void LevelLoaded(Level _level) {
            _level.Init(this);

            foreach (IController controller in controllers) {
                if (controller != null) {
                    controller.LevelLoaded(_level);
                }
            }
        }

        #region GameState

        public virtual void GameStateChanged(GameStateEnum _state) {
            //foreach (IController controller in controllers) {
            //    if (controller != null) {
            //        controller.GameStateChanged(_state);
            //    }
            //}
        }

        public virtual void SetGameState(GameStateEnum _state) {
            gameState.SetGameState(_state);
        }

        public virtual GameStateEnum GetGameState() {
            return gameState.state;
        }


        #endregion


        #region Awake/Start/Update Functions

        void Awake() {
            AwakeFunc();
        }
        protected virtual void AwakeFunc() { }

        void Start() {
            StartFunc();
        }
        protected virtual void StartFunc() { }

        void Update() {
            UpdateFunc();
        }
        protected virtual void UpdateFunc() { }

        void LateUpdate() {
            LateUpdateFunc();
        }
        protected virtual void LateUpdateFunc() { }

        #endregion
    }

    public interface IMainController {//Сейчас этот интерфейс нахуй не нужен, возможно в будущем тут что-то будет полезное, хз.
        public void Init();

        #region GameState
        public void GameStateChanged(GameStateEnum _state);
        public void SetGameState(GameStateEnum _state);
        public GameStateEnum GetGameState();
        #endregion
    }

    public interface IController {
        public void Init(IMainController _mainController);
        public void GameStateChanged(GameStateEnum _state);
        public void LevelLoaded(Level _level);
    }
}