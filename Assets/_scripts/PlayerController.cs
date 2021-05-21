using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    CharacterController characterController;
    GameObject playerCamera;

    float cameraRotation;
    public float speed = 10f;
    public float jumpForce = 1f;
    float _gravidade = 9.8f;
    float y;

    float Awareness, MaxAwareness;
    public AwarenessBehaviour awarenessBar;
    private void Start() {
        characterController = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        cameraRotation = 0.0f;
        Awareness = MaxAwareness;
        awarenessBar.SetAwareness(Awareness, MaxAwareness);
    }
    public void Reset() {
        transform.position = new Vector3(0, 2, 0);
    }

    public void Move() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");
        Mathf.Clamp(cameraRotation, -75.0f, 75.0f);
        cameraRotation += mouse_dY;

        if ( characterController.isGrounded && y < 0 ) {
            y = -2f;
        }

        if ( Input.GetButtonDown("Jump") && characterController.isGrounded == true ) {
            y = jumpForce;
        }
        if ( Input.GetKeyDown(KeyCode.LeftShift) /*&& stamina >= 1*/) {
            speed = 20.0f;
        }
        if ( Input.GetKeyUp(KeyCode.LeftShift) /*|| stamina <= 1*/) {
            speed = 10.0f;
        }

        if ( Input.GetKeyDown(KeyCode.LeftControl) ) {
            speed = 5.0f;
            playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        }
        if ( Input.GetKeyUp(KeyCode.LeftControl) ) {
            speed = 10.0f;
            playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + 0.69f, transform.position.z);
        }

        Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;
        y -= _gravidade * Time.deltaTime;
        characterController.Move(direction * speed * Time.deltaTime);

        transform.Rotate(Vector3.up, mouse_dX);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);
    }

    void Update() {
        Move();
    }

    void LateUpdate() {
        RaycastHit hit;
        Debug.DrawRay(transform.position, playerCamera.transform.forward * 5.0f, Color.magenta);

        if ( Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 10.0f) ) {
        }

    }

}