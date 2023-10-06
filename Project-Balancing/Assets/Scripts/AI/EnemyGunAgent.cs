using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GunEnemyState
{
    idle,
    chase,
    attack,
}
public class EnemyGunAgent : AiAgent
{
    EnemyRaycastWeapon weapon;
    GunEnemyState currentState;
    public GunEnemyState initialState = GunEnemyState.idle;
    GameObject playerObj;

    public float detectionRange = 15f;
    public float sightRange = 10f;
    public float attackRange = 5f;

    public override void Awake()
    {
        base.Awake();
        weapon = GetComponentInChildren<EnemyRaycastWeapon>();
        playerObj = FindObjectOfType<PlayerLocomotion>().gameObject;
    }

    public override void Update()
    {
        base.Update();
        switch (currentState)
        {
            case GunEnemyState.idle:
                IdleUpdate();
                break;
            case GunEnemyState.chase:
                ChaseUpdate();
                break;
            case GunEnemyState.attack:
                AttackUpdate();
                break;
        }
    }

    void IdleUpdate()
    {
        if (DistanceToPlayer() < detectionRange)
        {
            currentState = GunEnemyState.chase;
            return;
        }
    }

    void ChaseUpdate()
    {
        if (DistanceToPlayer() < attackRange)
        {
            currentState = GunEnemyState.attack;
            return;
        }
        navMeshAgent.SetDestination(playerObj.transform.position);
    }

    void AttackUpdate()
    {
        if (playerObj.GetComponent<BasicHealthManager>().isDead)
        {
            this.enabled = false;
            return;
        }
            
        if (DistanceToPlayer() > attackRange)
        {
            currentState = GunEnemyState.chase;
            return;
        }
        FacePlayer();
        weapon.Fire();
    }

    float DistanceToPlayer()
    {
        return (playerObj.transform.position - weapon.transform.position).magnitude;
    }

    void FacePlayer()
    {
        transform.LookAt(playerObj.transform.position);
    }
}
