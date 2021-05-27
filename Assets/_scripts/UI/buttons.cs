using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttons : MonoBehaviour
{
    GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
    }

    public void PlayGame() {
        gm.changeState(GameManager.GameState.GAME);
    }

    public void GoToOptions() {
        gm.changeState(GameManager.GameState.OPTIONS);
    }

    public void GoToTutorial() {
        gm.changeState(GameManager.GameState.TUTORIAL);
    }

    public void PauseGame() {
        gm.changeState(GameManager.GameState.PAUSE);
    }

    public void GoToMainMenu() {
        gm.changeState(GameManager.GameState.MENU);
    }
}
