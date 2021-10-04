namespace VladB.GameTemplate {
    public class GameStateEnums{
    }
    public enum GameStateEnum {
        //При добавлении новых элементов обязательно ресетнуть списки в MainController'e
        None,
        BeginUnloadingLevel,
        EndUnloadingLevel,

        BeginLoadLevel,
        LevelLoaded,
        LevelInitialized,

        Start,
        Game,

        BeginGameOver,
        GameOver,

        Pausing,
        Pause,
        UnPausing
    }
}
