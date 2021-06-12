using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_End : MonoBehaviour {
    public Text textComp;
    GameManager gm;

    // Start is called before the first frame update
    void Start() {
        //textComp = GetComponent<Text>();
        gm = GameManager.GetInstance();
    }

    // Update is called once per frame
    void Update() {
        if (gm.life <= 0) {
            textComp.text = $"Lose";
        } else {
            textComp.text = $"Win";
        }
    }
}
