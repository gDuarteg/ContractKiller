public class GameManager {

    private static GameManager _instance;

    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;

    //public SoundFXManager soundFxMgr { get; set; }
    //SoundFXManager.ClipName currentSoundFx = SoundFXManager.ClipName.IDLE;

    public enum GameState { MENU, GAME, PAUSE, ENDGAME, OPTIONS, TUTORIAL };
    public enum EndGameState { WON, LOST };

    public EndGameState endGameStatus { get; private set; }
    public GameState currentState { get; private set; }

    public PlayerController player { get; set; }

    public float remainingTime = 20.0f;
    public int life = 20;


    private GameManager() {
        currentState = GameState.MENU;
        endGameStatus = EndGameState.LOST;
    }

    public static GameManager GetInstance() {
        if (_instance == null) {
            _instance = new GameManager();
        }
        return _instance;
    }

    public void changeState(GameState nextState) {
        if (currentState != GameState.PAUSE && currentState != GameState.OPTIONS && currentState != GameState.TUTORIAL && nextState == GameState.GAME) Reset();
        currentState = nextState;
        changeStateDelegate();
    }

    public void Reset() {
        endGameStatus = EndGameState.LOST;
        remainingTime = 60.0f;
        //changeStateDelegate();
        //player.Reset();
    }
    public void EndGame() {
        if (life <= 0f) {
            endGameStatus = EndGameState.LOST;
        }
        else {
            endGameStatus = EndGameState.WON;
        }
        changeState(GameManager.GameState.ENDGAME);
    }

    //public void SetSoundFx(SoundFXManager.ClipName clip) {
    //    if (currentSoundFx != SoundFXManager.ClipName.IDLE && !SoundFXManager.audioSrc.isPlaying && currentSoundFx == clip) {
    //        if (clip != SoundFXManager.ClipName.WIN && clip != SoundFXManager.ClipName.LOOSE) {
    //            SoundFXManager.PlaySound(clip);

    //        }
    //    }
    //    if (currentSoundFx != clip) {
    //        currentSoundFx = clip;
    //        SoundFXManager.PlaySound(clip);
    //    }
    //}

}