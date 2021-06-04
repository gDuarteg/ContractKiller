using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public float expiryTime = 0f;

    // Start is called before the first frame update
    void Start() {
        Destroy(gameObject , expiryTime);
    }
    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.collider.tag);
        if ( collision.collider.name == "Player" ) {
            collision.collider.GetComponent<PlayerController>().onHit();
        }
    }

    // Update is called once per frame
    void Update() {

    }
}
