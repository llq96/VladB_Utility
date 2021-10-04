using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using VladB.Utility;

namespace VladB.GameTemplate {
    [RequireComponent(typeof(GameState))]
    public class MainController : MonoBehaviour {
        [HideInInspector] public GameState gameState;

        [Header("Objects With IController")]
        [HideInInspector] public List<GameObject> controllersObjects = new List<GameObject>();
        List<IController> controllers = new List<IController>();

        [Header("Execution Order for OnGameStateChanged")]
        [HideInInspector] public List<ListOfControllers> listsByState = new List<ListOfControllers>();


        public virtual void Init() {
            gameState = GetComponent<GameState>();
            gameState.OnGameStateChanged -= GameStateChanged;
            gameState.OnGameStateChanged += GameStateChanged;

            //Получаем контроллеры из GameObject'ов
            controllers = controllersObjects.Where(go => go != null)
                                            .Select(go => go.GetComponent<IController>())
                                            .Where(c => c != null)
                                            .ToList();

            //Инициализируем контроллеры
            controllers.Act(x => x?.Init(this));
        }

        #region GameState
        protected virtual void GameStateChanged(GameStateEnum state, params object[] parameters) {
            //Проходимся по списку контроллеров для конкретного GameStateEnum и вызываем в них GameStateChanged
            listsByState.First(x => x.gameState == state).controllers
                .Act(item => item.GameStateChanged(state, parameters));
        }

        public virtual GameStateEnum GetGameState() => gameState.state;
        public virtual void SetGameState(GameStateEnum state, params object[] parameters) => gameState.SetGameState(state, parameters);
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



    public interface IController {
        public void Init(MainController mainController);
        public void GameStateChanged(GameStateEnum state, params object[] parameters);
    }


    [System.Serializable]
    public class ListOfControllers {
        [HideInInspector] public string __editor_name;//For Editor Only
        [HideInInspector] public GameStateEnum gameState;

        public List<GameObject> controllersObjects = new List<GameObject>();

        [HideInInspector] public bool isNeedUpdateList = true;

        List<IController> __controllers = new List<IController>();
        public List<IController> controllers {
            get {
                if(isNeedUpdateList) {
                    __controllers = controllersObjects.Select(go => go.GetComponent<IController>()).ToList();
                    isNeedUpdateList = false;
                }
                return __controllers;
            }
            private set {
                __controllers = value;
            }
        }


        public ListOfControllers(GameStateEnum gameState) {
            this.gameState = gameState;
            UpdateEditorName();
        }

        public void UpdateEditorName(bool isArrayModified = false) {
            __editor_name = $"{gameState}{(isArrayModified ? "          (MODIFIED)" : "")}";
        }
    }
}