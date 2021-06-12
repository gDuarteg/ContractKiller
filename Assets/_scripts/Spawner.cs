using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject inimigo;
    Vector3 posicao;
    GameManager gm;

    void Start() {
        gm = GameManager.GetInstance();
        GameManager.changeStateDelegate += Spawn;
        Spawn();
    }

    void Spawn() {
        if (gm.currentState == GameManager.GameState.GAME) {
            foreach (Transform child in transform) {
                GameObject.Destroy(child.gameObject);
            }
            posicao = new Vector3(-7, -6, -70);
            Instantiate(inimigo, posicao, Quaternion.identity, transform);

            posicao = new Vector3(-35, -6, -70);
            Instantiate(inimigo, posicao, Quaternion.identity, transform);

            posicao = new Vector3(-35, -6, -40);
            Instantiate(inimigo, posicao, Quaternion.identity, transform);

            posicao = new Vector3(0, -6, -31);
            Instantiate(inimigo, posicao, Quaternion.identity, transform);

            posicao = new Vector3(3, -6, -31);
            Instantiate(inimigo, posicao, Quaternion.identity, transform);

            //posicao = new Vector3(16.4f,-4.1f,-57.12f);
            //Instantiate(inimigo, posicao, Quaternion.identity, transform);

        }
    }

    void Update() {
        if (transform.childCount <= 0 && gm.currentState == GameManager.GameState.GAME) {
            Spawn();
        }

    }
}
