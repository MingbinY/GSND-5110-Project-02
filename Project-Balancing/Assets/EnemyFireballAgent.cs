using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum FireballEnemyState
{
    idle,
    chase,
    attack,
}
public class EnemyFireballAgent : AiAgent
{
    public LayerMask ignoreLayer;
    public GameObject fireballPrefab;
    FireballEnemyState currentState;
    public FireballEnemyState initialState = FireballEnemyState.idle;
    GameObject playerObj;
    public Transform fireballPos;

    public float detectionRange = 15f;
    public float sightRange = 10f;
    public float attackRange = 5f;
    public float attackStopRange = 3f;

    public float attackInterval = 5f;
    public float lastAttackTime = 0;

    public override void Awake()
    {
        base.Awake(); 
        playerObj = FindObjectOfType<PlayerLocomotion>().gameObject;
    }

    public override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case FireballEnemyState.idle:
                IdleUpdate();
                break;
            case FireballEnemyState.chase:
                ChaseUpdate();
                break;
            case FireballEnemyState.attack:
                AttackUpdate();
                break;
        }
    }

    void IdleUpdate()
    {
        if (DistanceToPlayer() < detectionRange && CanSeePlayer())
        {
            currentState = FireballEnemyState.chase;
            return;
        }
    }

    void ChaseUpdate()
    {
        navMeshAgent.stoppingDistance = 0;
        if (DistanceToPlayer() < attackRange && CanSeePlayer())
        {
            currentState = FireballEnemyState.attack;
            return;
        }
        navMeshAgent.SetDestination(playerObj.transform.position);
    }

    void AttackUpdate()
    {
        navMeshAgent.stoppingDistance = attackRange;
        if (playerObj.GetComponent<BasicHealthManager>().isDead)
        {
            this.enabled = false;
            return;
        }

        if (DistanceToPlayer() > attackRange)
        {
            currentState = FireballEnemyState.chase;
            return;
        }
        if (!CanSeePlayer())
        {
            currentState = FireballEnemyState.chase;
            return;
        }
        FacePlayer();
        if (Time.time > lastAttackTime + attackInterval)
        {
            Instantiate(fireballPrefab, fireballPos.position, transform.rotation);
            lastAttackTime = Time.time;
        }    
    }

    float DistanceToPlayer()
    {
        return (playerObj.transform.position - transform.position).magnitude;
    }

    void FacePlayer()
    {
        transform.LookAt(playerObj.transform.position);
    }

    bool CanSeePlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerObj.transform.position - transform.position, out hit, sightRange, ~ignoreLayer))
        {
            if (hit.collider.gameObject == playerObj)
            {
                return true;
            }
        }

        return false;
    }
}
