using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiAgent : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;
    protected BasicHealthManager healthManager;

    public virtual void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthManager = GetComponent<BasicHealthManager>();
    }

    public virtual void Start() { }
    public virtual void Update() {
        if (healthManager.isDead)
            return;
    }
}
