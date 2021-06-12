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
        gm.life = 40;
    }

    public void GoToOptions() {
        gm.changeState(GameManager.GameState.OPTIONS);
    }

    public void GoToTutorial() {
        gm.changeState(GameManager.GameState.TUTORIAL);
    }

    public void UnPauseGame() {
        Time.timeScale = 1;
        gm.changeState(GameManager.GameState.GAME);
    }

    public void GoToMainMenu() {
        gm.changeState(GameManager.GameState.MENU);
    }
}
