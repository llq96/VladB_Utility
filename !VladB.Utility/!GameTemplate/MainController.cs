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
            gameState.OnGameStateChanged += GameStateChanged;

            IController temp;
            foreach(GameObject go in controllersObjects) {
                if(go) {
                    temp = go.GetComponent<IController>();
                    if(temp != null) {
                        controllers.Add(temp);
                    } else {
                        if(isDebugLog) {
                            Debug.LogError($"{go.name} does not have IController !");
                        }
                    }
                } else {
                    if(isDebugLog) {
                        Debug.LogError("GameObjet is null! ");
                    }
                }
            }

            foreach(IController controller in controllers) {
                if(controller != null) {
                    controller.Init(this);
                    gameState.OnGameStateChanged -= controller.GameStateChanged;
                    gameState.OnGameStateChanged += controller.GameStateChanged;
                    if(isDebugLog) {
                        Debug.Log($"Init {controller}");
                    }
                } else {
                    if(isDebugLog) {
                        Debug.LogError("controller is null !");
                    }
                }
            }
        }

        public void LevelLoaded(Level level) {
            level.Init(this);

            foreach(IController controller in controllers) {
                if(controller != null) {
                    controller.LevelLoaded(level);
                }
            }
        }

        #region GameState
        public virtual void GameStateChanged(GameStateEnum state) { }

        public virtual void SetGameState(GameStateEnum state) {
            gameState.SetGameState(state);
        }

        public virtual GameStateEnum GetGameState() {
            return gameState.state;
        }
        #endregion

        #region Awake/Start/Update Functions
        void Awake() => AwakeFunc();
        protected virtual void AwakeFunc() { }

        void Start() => StartFunc();
        protected virtual void StartFunc() { }

        void Update() => UpdateFunc();
        protected virtual void UpdateFunc() { }

        void LateUpdate() => LateUpdateFunc();
        protected virtual void LateUpdateFunc() { }
        #endregion
    }


    public interface IMainController {//—ейчас этот интерфейс совсем не нужен, возможно в будущем тут что-то будет полезное.
        public void Init();

        #region GameState
        public void GameStateChanged(GameStateEnum state);
        public void SetGameState(GameStateEnum state);
        public GameStateEnum GetGameState();
        #endregion
    }


    public interface IController {
        public void Init(IMainController iMainController);
        public void GameStateChanged(GameStateEnum state);
        public void LevelLoaded(Level level);
    }
}