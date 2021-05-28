using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour {
    Animator animator;

    float EnemyView = 5f;
    public float health;

    public GameObject Player;
    protected NavMeshAgent Agent;
    protected StateEnum State;
    protected Target[] PotentialTargets;
    public Target target;
    protected float NextState;

    void Awake() {
        Agent = GetComponent<NavMeshAgent>();
        PotentialTargets = FindObjectsOfType<Target>();
        target = PotentialTargets[Random.Range(0, PotentialTargets.Length)];
        Agent.SetDestination(target.transform.position);
        State = StateEnum.RUN;
    }

    private void Start() {
        animator = GetComponent<Animator>();
        health = 10f;
    }

    void Update() {
        //animator.SetBool("Run", false);
        //animator.SetBool("Shoot", false);

        Agent.updatePosition = true;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        NextState -= Time.deltaTime;

        switch (State) {
            case StateEnum.RUN:
                animator.SetBool("Run", true);
                if (Vector3.Distance(Player.transform.position, transform.position) < EnemyView) {
                    State = StateEnum.SHOOT;
                    NextState = Random.Range(2f, 5f);
                    animator.SetBool("Run", false);
                }
                else {
                    transform.position += Agent.desiredVelocity * Time.deltaTime;
                }
                break;

            case StateEnum.SHOOT:
                animator.SetBool("Shoot", true);
                var look = Player.transform.position - transform.position;

                if (Vector3.Distance(Player.transform.position, transform.position) > EnemyView) {
                    State = StateEnum.RUN;
                    var targetIndex = Random.Range(0, PotentialTargets.Length);
                    //    for (var i = 0; i < PotentialTargets.Length && PotentialTargets[targetIndex].Occupied; i++)
                    //        targetIndex = (targetIndex + 1) % PotentialTargets.Length;
                    target = PotentialTargets[targetIndex];
                    //    target.EnemyGoal = this;
                    Agent.SetDestination(target.transform.position);

                    animator.SetBool("Shoot", false);
                }
                break;
        }


        //Debug.DrawLine(transform.position + Vector3.up, transform.position + Vector3.up + Agent.desiredVelocity * 10);
    }

    public enum StateEnum {
        RUN,
        SHOOT
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if (health <= 0) {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy() {
        Destroy(gameObject);
    }
}