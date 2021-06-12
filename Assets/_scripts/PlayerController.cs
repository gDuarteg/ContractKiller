using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    GameManager gm;
    CharacterController characterController;
    GameObject playerCamera;
    public GameObject Objetivo;
    float cameraRotation;
    public float speed = 10f;
    public float jumpForce = 1f;
    float _gravidade = 9.8f;
    float y;

    //public GameObject hostage;
    float Awareness, MaxAwareness;
    public AwarenessBehaviour awarenessBar;

    bool podeGanhar;
    public KeyCode TeclaGanhar = KeyCode.F;

    private void Start() {
        gm = GameManager.GetInstance();
        GameManager.changeStateDelegate += Reset;
        GameManager.changeStateDelegate2 += Reset;

        characterController = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        cameraRotation = 0.0f;
        Awareness = MaxAwareness;
        awarenessBar.SetAwareness(Awareness , MaxAwareness);
    }
    public void Reset() {
        transform.position = new Vector3(23 , -6 , -71);
        //gm.life = 20;
    }

    public void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = -Input.GetAxis("Mouse Y");
        Mathf.Clamp(cameraRotation , 75.0f , -75.0f);
        cameraRotation += mouse_dY;

        if ( characterController.isGrounded && y < 0 ) {
            y = -2f;
        }

        if ( Input.GetButtonDown("Jump") && characterController.isGrounded == true ) {
            y = jumpForce;
        }
        if ( Input.GetKeyDown(KeyCode.LeftShift) /*&& stamina >= 1*/) {
            speed = 15.0f;
        }
        if ( Input.GetKeyUp(KeyCode.LeftShift) /*|| stamina <= 1*/) {
            speed = 10.0f;
        }

        if ( Input.GetKeyDown(KeyCode.LeftControl) ) {
            speed = 5.0f;
            playerCamera.transform.position = new Vector3(transform.position.x , transform.position.y - 0.5f , transform.position.z);
        }
        if ( Input.GetKeyUp(KeyCode.LeftControl) ) {
            speed = 10.0f;
            playerCamera.transform.position = new Vector3(transform.position.x , transform.position.y + 0.69f , transform.position.z);
        }

        Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;
        y -= _gravidade * Time.deltaTime;
        characterController.Move(direction * speed * Time.deltaTime);

        transform.Rotate(Vector3.up , mouse_dX);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation , 0.0f , 0.0f);
    }

    void Hit() {
        gm.life -= 1;
    }

    void Update() {
        //if (Input.GetKeyDown(KeyCode.F)) {
        //    gm.changeState(GameManager.GameState.ENDGAME);
        //}
        if ( gm.life <= 0 ) {
            gm.EndGame();
        }
        if ( gm.currentState != GameManager.GameState.GAME ) {
            if ( Cursor.lockState == CursorLockMode.Locked ) {
                Cursor.lockState = CursorLockMode.None;
            }
            return;
        } else {
            if ( Cursor.lockState != CursorLockMode.Locked ) {
                Cursor.lockState = CursorLockMode.Locked;
            }

        }

        if ( Input.GetKeyDown(KeyCode.Escape) ) {
            Time.timeScale = 0;
            gm.changeState(GameManager.GameState.PAUSE);
            return;
        }

        Move();
        Fim();

        //RaycastHit hit;
        Debug.DrawRay(transform.position, playerCamera.transform.forward * 5.0f, Color.magenta);

        //if (Input.GetKeyDown(KeyCode.E)) {
        //    if (gm.currentState == GameManager.GameState.GAME) {
        //        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 5.0f)) {
        //            gm.changeState(GameManager.GameState.ENDGAME);
        //        }
        //    }
        //}
    }
    public void onHit() {
        //Debug.Log("MORRE");
        gm.life -= 1;
    }

    void LateUpdate() {
        RaycastHit hit;

        Debug.DrawRay(transform.position, playerCamera.transform.forward * 5.0f, Color.magenta);

        //if (Input.GetKeyDown(KeyCode.F)) {
        //    if (gm.currentState == GameManager.GameState.GAME) {
        //        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 5.0f)) {
        //            gm.changeState(GameManager.GameState.ENDGAME);
        //        }
        //    }
        //}

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10.0f)) {
        }

    }

    void Fim() {
        float distancia = Vector3.Distance(transform.position, Objetivo.transform.position);
        if (distancia < 3) {
            podeGanhar = true;
        }
        else {
            podeGanhar = false;
        }
        if (Input.GetKeyDown(TeclaGanhar) && podeGanhar) {
            gm.EndGame();
        }
    }

    void OnGUI() {
        if (podeGanhar == true && gm.currentState == GameManager.GameState.GAME) {
            GUIStyle stylez = new GUIStyle();
            stylez.alignment = TextAnchor.MiddleCenter;
            GUI.skin.label.fontSize = 20;
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 200, 30), "Pressione: " + TeclaGanhar);
        }
    }

}