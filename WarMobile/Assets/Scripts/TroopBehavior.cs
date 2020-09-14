using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class TroopBehavior : MonoBehaviour
{
    [SerializeField] private float attackRange;
    [SerializeField] private bool isVehicle = false;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform targetDestination;
    
    private void Awake()
    {
        if (!isVehicle)
            animator = GetComponent<Animator>();

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (enabled)
        {
            GoToTargetDestination();
        }
    }

    private void OnEnable()
    {
        // For paratrooping troops.
        GoToTargetDestination();
    }

    private void GoToTargetDestination()
    {
        targetDestination = GameObject.FindGameObjectWithTag("PlayerDestination").transform;

        navMeshAgent.enabled = true;
        navMeshAgent.Warp(transform.position);
        navMeshAgent.SetDestination(targetDestination.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isVehicle)
            animator.SetFloat("speed", navMeshAgent.velocity.magnitude);
    }
}
