using UnityEngine;
using Box.Player;
using UnityEngine.AI;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerIdentifier))]
public class CrawlerAI : MonoBehaviour
{
    [SerializeField] Transform body;
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject projectile;
    Health health;
    PlayerAttack player;
    NavMeshAgent agent;
    PlayerIdentifier identifier;
    Animator animator;
    [SerializeField] Slider healthBar;


    
    CurrentAiState current = CurrentAiState.Wander;
    private float timer;
    [SerializeField] float wanderRadius;
    [SerializeField] float wanderTimer;
    [SerializeField] float offset=2;
    public int damage = 1;
    bool attackinprogress = false;



    private void Awake()
    {
        player = FindAnyObjectByType<PlayerAttack>();
        health = GetComponent<Health>();
        agent = GetComponent<NavMeshAgent>();
        identifier = GetComponent<PlayerIdentifier>();
        animator = GetComponent<Animator>();
        timer = wanderTimer;

        agent.updateRotation = false;
        agent.updateUpAxis = false;


    }

    private void Start()
    {

        healthBar.maxValue = health.currentHealthReturn();
        healthBar.value = health.currentHealthReturn();
        healthBar.gameObject.active=false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerMelee") { healthBar.gameObject.active = true; healthBar.value= health.currentHealthReturn(); health.DealDamage(player.attackAmount, Mathf.RoundToInt(player.gameObject.GetComponent<PlayerMovement>().body.transform.localScale.x)); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerRange") { healthBar.gameObject.active = true; healthBar.value = health.currentHealthReturn(); health.DealDamage(player.rangeAmount, 0); }

    }

    private void FixedUpdate()
    {
        switch (current) 
        {
            case CurrentAiState.Wander:
                Wander();
                break;
            case CurrentAiState.Chase:
                Chase();
                break;
            case CurrentAiState.Stun:
                break;
        }
        if (agent.velocity.x > 0) { body.localScale = new Vector2(-1, 1); }
        else if (agent.velocity.x < 0) { body.localScale = new Vector2(1, 1); }

        if (identifier.AwareOfPlayer) { current = CurrentAiState.Chase; }
        else { current = CurrentAiState.Wander; }

    }

    private void Wander() 
    {
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, 1);
            agent.SetDestination(newPos);
            timer = 0;
        }





    }

    private void Chase() 
    {
        agent.SetDestination(player.gameObject.transform.position);

        if (Vector2.Distance(player.gameObject.transform.position, transform.position) < offset) 
        {
            agent.isStopped = true;
            if (attackinprogress) { return; }
            animator.SetTrigger("Attack");

        }
        else 
        {
            agent.isStopped = false;
            attackinprogress = false;

        }


    }



    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    public void attackToggle() 
    {
        attackinprogress = !attackinprogress;
    }


    public void SpawnProjectile()
    {
        var currentRot = Quaternion.EulerRotation(new Vector3(90, 0, 0));

        if (transform.localScale.x == 1) { currentRot = transform.rotation; }


        GameObject b = Instantiate(projectile, shootPoint.position, currentRot);
        b.GetComponent<Rigidbody2D>().AddForce((Vector2.left * body.transform.localScale.x) * 1f, ForceMode2D.Impulse);
        Destroy(b, 2f);
    }



}

enum CurrentAiState {Wander,Chase,Stun }