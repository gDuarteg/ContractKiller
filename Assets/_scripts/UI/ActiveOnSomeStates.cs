using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActiveOnSomeStates : MonoBehaviour 
{ 
    public GameManager.GameState[] activeStates;
    GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
        GameManager.changeStateDelegate += UpdateVisibility;
        UpdateVisibility();
    }

    void UpdateVisibility() {
        if (activeStates.Contains(gm.currentState)) {
            gameObject.SetActive(true);
        }
        else {
            gameObject.SetActive(false);
        }
    }
}