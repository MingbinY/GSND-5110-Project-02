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
    public float attackStopRange = 3f;

    public GameObject weaponPrefab;
    GameObject weaponObj;
    public GameObject weaponSlot;

    public override void Awake()
    {
        base.Awake();
        weaponObj = Instantiate(weaponPrefab, weaponSlot.transform.position, weaponSlot.transform.rotation, weaponSlot.transform);
        weapon = weaponObj.GetComponent<EnemyRaycastWeapon>();
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
        if (DistanceToPlayer() < detectionRange && CanSeePlayer())
        {
            currentState = GunEnemyState.chase;
            return;
        }
    }

    void ChaseUpdate()
    {
        navMeshAgent.stoppingDistance = 0;
        if (DistanceToPlayer() < attackRange && CanSeePlayer())
        {
            currentState = GunEnemyState.attack;
            return;
        }
        navMeshAgent.SetDestination(playerObj.transform.position);
    }

    void AttackUpdate()
    {
        navMeshAgent.stoppingDistance = Random.Range(attackStopRange, attackRange);
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
        if (!CanSeePlayer())
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

    bool CanSeePlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, playerObj.transform.position - transform.position, out hit))
        {
            if (hit.collider.gameObject == playerObj)
            {
                return true;
            }
        }

        return false;
    }
}
