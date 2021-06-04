using UnityEngine;
using System.Collections;

public class Inimigo : MonoBehaviour {

    public float vida = 100;
    bool chamouMorte = false;
    Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (vida <= 0) {
            vida = 0;
            if (chamouMorte == false) {
                chamouMorte = true;
                StartCoroutine("Morrer");
            }
        }
    }

    IEnumerator Morrer() {
        GetComponent<MeshRenderer>().material.color = Color.red;
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}