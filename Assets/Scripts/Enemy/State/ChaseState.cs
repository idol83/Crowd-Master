using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
public class ChaseState : EnemyState
{
    private NavMeshAgent _navMeshAgent;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void OnEnable()
    {
        _navMeshAgent.enabled = true;
        Animator.SetTrigger("Run");
    }
    private void OnDisable()
    {
        _navMeshAgent.enabled = false;
    }
    private void Update()
    {
        _navMeshAgent.SetDestination(Player.transform.position);
    }
}
