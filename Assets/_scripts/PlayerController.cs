using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    float _baseSpeed = 10.0f;
    float _gravidade = 9.8f;
    float jumpForce = 300;
    //bool doubleJump = true;
    //float stamina = 100f;
    //float maxStamina = 100f;

    //GameManager gm;
    CharacterController characterController;
    public Rigidbody rb;

    GameObject playerCamera;
    float cameraRotation;

    //Vector3 warpPosition = Vector3.zero;

    public void Reset() {
        transform.position = new Vector3(0, 2, 0);
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        //gm = GameManager.GetInstance();
        //gm.player = this;

        characterController = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        cameraRotation = 0.0f;

        //staminabar.SetStamina(stamina, maxStamina);
    }

    void Update() {
        //if (gm.currentState != GameManager.GameState.GAME) {
        //    if (minimapa.active) {
        //        minimapa.SetActive(false);
        //    }
        //    return;
        //}
        //else {
        //    if (!minimapa.active) {
        //        minimapa.SetActive(true);
        //    }

        //}

        //if (Input.GetKeyDown(KeyCode.Escape) && gm.currentState == GameManager.GameState.GAME) {
        //    gm.changeState(GameManager.GameState.PAUSE);
        //    gm.SetSoundFx(SoundFXManager.ClipName.IDLE);

        //    return;
        //}

        //gm.remainingTime -= 1 * Time.deltaTime;
        //if (gm.remainingTime <= 0) {
        //    gm.EndGame();
        //}

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = 0;

        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");

        cameraRotation += mouse_dY;
        Mathf.Clamp(cameraRotation, -75.0f, 75.0f);

        if (Input.GetKeyDown(KeyCode.LeftShift) /*&& stamina >= 1*/) {
            _baseSpeed = 20.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) /*|| stamina <= 1*/) {
            _baseSpeed = 10.0f;
        }

        //if (_baseSpeed == 20.0f && stamina >= 0) {
        //    stamina -= Time.deltaTime * 35;
        //    staminabar.SetStamina(stamina, maxStamina);
        //}
        //else if (_baseSpeed == 10.0f && stamina < maxStamina) {
        //    stamina += Time.deltaTime * 10f;
        //    staminabar.SetStamina(stamina, maxStamina);
        //}

        // Sound Fx
        //if (_baseSpeed == 20.0f) {
        //    gm.SetSoundFx(SoundFXManager.ClipName.RUNNING);
        //}
        //else if (x != 0 || z != 0) {
        //    gm.SetSoundFx(SoundFXManager.ClipName.WALKING);
        //}
        //else {
        //    gm.SetSoundFx(SoundFXManager.ClipName.IDLE);
        //}
        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            _baseSpeed = 5.0f;
            playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) {
            _baseSpeed = 10.0f;
            playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 0.69f, transform.position.z);
        }

        Vector3 moveDirection = new Vector3(x, 0, z);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= _baseSpeed;

        //if (characterController.isGrounded /*|| doubleJump == true*/) {
        //    if (Input.GetKeyDown(KeyCode.Space) && _baseSpeed == 10.0f) {
        //        rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        //        //moveDirection.y = jumpForce;
        //        //doubleJump = false;
        //    }
        //    else if (Input.GetKeyDown(KeyCode.Space) && _baseSpeed == 20.0f) {
        //        moveDirection.y = jumpForce * 2;
        //        //doubleJump = false;
        //    }
        //}

        //if (characterController.isGrounded && y < 0) {
        //    y = -2f;
        //}

        //if (Input.GetButtonDown("Jump") && characterController.isGrounded) {
        //    y = 3f;
        //}
        //Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;

        //y -= _gravidade * Time.deltaTime;

        //characterController.Move(direction * _baseSpeed * Time.deltaTime);

        if (!characterController.isGrounded) {
            moveDirection.y -= _gravidade;
        }

        //else {
        //    doubleJump = true;
        //}

        characterController.Move(moveDirection * Time.deltaTime);

        transform.Rotate(Vector3.up, mouse_dX);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);
    }

    void LateUpdate() {
        //if (gm.currentState != GameManager.GameState.GAME) {
        //    return;
        //}

        RaycastHit hit;
        Debug.DrawRay(transform.position, playerCamera.transform.forward * 5.0f, Color.magenta);
        
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 5.0f)) {
            //if (hit.collider.gameObject.Equals(chest)) {
            //    gm.EndGame();
            //    Reset();
            //}
        }
        
    }
}