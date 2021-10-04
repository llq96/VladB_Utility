namespace VladB.GameTemplate {
    public class GameStateEnums{
    }
    public enum GameStateEnum {
        //��� ���������� ����� ��������� ����������� ��������� ������ � MainController'e
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
