using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour {
    GameManager gm;
    Animator animator;
    public float vida = 100;
    bool chamouMorte = false;
    float EnemyView = 10f;
    public float health;
    bool chamouHit = false;
    public GameObject Player;
    protected NavMeshAgent Agent;
    protected StateEnum State;
    protected Target[] PotentialTargets;
    public Target target;
    protected float NextState;

    private float shootRateTimeStamp = 0f;
    public float shootRate = 0f;
    public float shootForce = 0f;

    public GameObject bullet;
    public GameObject weapon;

    void Awake() {
        Agent = GetComponent<NavMeshAgent>();
        PotentialTargets = FindObjectsOfType<Target>();
        Player = GameObject.Find("Player");
        target = PotentialTargets[Random.Range(0 , PotentialTargets.Length)];
        Agent.SetDestination(target.transform.position);
        State = StateEnum.RUN;
    }

    private void Start() {
        gm = GameManager.GetInstance();
        animator = GetComponent<Animator>();
        health = 10f;
    }

    void Update() {
        if ( gm.currentState != GameManager.GameState.GAME ) {
            return;
        }
        if ( vida <= 0 ) {
            vida = 0;
            if ( chamouMorte == false ) {
                chamouMorte = true;
                DestroyEnemy();
            }
        }
        //animator.SetBool("Run", false);
        //animator.SetBool("Shoot", false);

        Agent.updatePosition = true;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;

        NextState -= Time.deltaTime;
        Vector3 targetDirection = transform.forward;

        switch ( State ) {
            case StateEnum.RUN:
            animator.SetBool("Run" , true);
            if ( Vector3.Distance(Player.transform.position , transform.position) < EnemyView || NextState < 0 ) {
                State = StateEnum.SHOOT;
                NextState = Random.Range(2f , 5f);
                animator.SetBool("Run" , false);
            }
            //else {
            //    transform.position += Agent.desiredVelocity * Time.deltaTime;
            //}
            targetDirection = Agent.desiredVelocity;
            break;

            case StateEnum.SHOOT:
            if ( Time.time > shootRateTimeStamp ) {
                GameObject go = (GameObject) Instantiate(bullet , weapon.transform.position, weapon.transform.rotation);
                go.GetComponent<Rigidbody>().AddForce(weapon.transform.right * shootForce);
                shootRateTimeStamp = Time.time + shootRate;
            }
            animator.SetBool("Run" , false);
            animator.SetBool("Shoot" , true);
            var look = Player.transform.position - transform.position;
            //Hit();
            //if ( chamouHit == false ) {
            //    chamouHit = true;
            //    StartCoroutine("Hit");
            //}
            targetDirection = look;
            if ( Vector3.Distance(Player.transform.position , transform.position) > EnemyView ) {
                State = StateEnum.RUN;
                var targetIndex = Random.Range(0 , PotentialTargets.Length);
                //for (var i = 0; i < PotentialTargets.Length && PotentialTargets[targetIndex].Occupied; i++)
                //    targetIndex = (targetIndex + 1) % PotentialTargets.Length;
                target = PotentialTargets[targetIndex];
                //    target.EnemyGoal = this;
                Agent.SetDestination(target.transform.position);

                animator.SetBool("Shoot" , false);
            }
            break;
        }

        transform.position += Agent.desiredVelocity * Time.deltaTime;
        //transform.rotation



        // The step size is equal to speed times frame time.
        float singleStep = 2 * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward , targetDirection , singleStep , 0.0f);

        // Draw a ray pointing at our target in
        Debug.DrawRay(transform.position , newDirection , Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);

        Debug.DrawLine(transform.position + Vector3.up , transform.position + Vector3.up + Agent.desiredVelocity * 10);
    }

    public enum StateEnum {
        RUN,
        SHOOT
    }

    public void TakeDamage(int damage) {
        health -= damage;
        if ( health <= 0 ) {
            DestroyEnemy();
        }
    }

    //IEnumerator Hit() {
    //    //GetComponent<MeshRenderer>().material.color = Color.red;
    //    yield return new WaitForSeconds(2);
    //    gm.life -= 1;
    //}
    private void DestroyEnemy() {
        Destroy(gameObject);
    }
    void Hit() {
        gm.life -= 1;
    }
}