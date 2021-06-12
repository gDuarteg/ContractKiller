public class GameManager {

    private static GameManager _instance;

    public delegate void ChangeStateDelegate();
    public delegate void ChangeStateDelegate2();

    public static ChangeStateDelegate changeStateDelegate;
    public static ChangeStateDelegate changeStateDelegate2;


    //public SoundFXManager soundFxMgr { get; set; }
    //SoundFXManager.ClipName currentSoundFx = SoundFXManager.ClipName.IDLE;

    public enum GameState { MENU, GAME, PAUSE, ENDGAME, OPTIONS, TUTORIAL };
    //public enum EndGameState { WON, LOST };

    //public EndGameState endGameStatus { get; private set; }
    public GameState currentState { get; private set; }

    public PlayerController player { get; set; }

    public float remainingTime = 20.0f;
    public int life = 40;


    private GameManager() {
        currentState = GameState.MENU;
        //endGameStatus = EndGameState.LOST;
    }

    public static GameManager GetInstance() {
        if (_instance == null) {
            _instance = new GameManager();
        }
        return _instance;
    }

    public void changeState(GameState nextState) {
        //if (currentState != GameState.PAUSE && currentState != GameState.OPTIONS && currentState != GameState.TUTORIAL && nextState == GameState.GAME) Reset();
        currentState = nextState;
        changeStateDelegate();
        
    }
    //public void Reset() {
    //    //endGameStatus = EndGameState.LOST;
    //    remainingTime = 60.0f;
    //    //changeStateDelegate();
    //    //player.Reset();
    //}
    public void EndGame() {
        changeState(GameManager.GameState.ENDGAME);
    }
}