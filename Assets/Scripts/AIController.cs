
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private Vector3 initialPosition;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isArrived;

    private enum EState
    {
        Idle,
        Running,
        Stopped,
        Arrived
    }

    private void Start()
    {
        initialPosition = transform.position;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.enabled = false;
        isArrived = false;
    }

    private void Update()
    {
        if (navMeshAgent.enabled && !isArrived)
        {
            animator.SetBool("IsRunning", true);
            if (ActualRemainingDistance() < 0.3f)
            {

                animator.SetBool("IsRunning", false);
                animator.SetTrigger("Arrived");
                isArrived = true;
            }
            else
            {
                //navMeshAgent.isStopped = false;
                animator.SetBool("IsRunning", true);
            }
        }

    }
    private float ActualRemainingDistance()
    {
        return Vector3.Distance(navMeshAgent.transform.position, target.transform.position);
    }

    public void StartMoving()
    {
        navMeshAgent.enabled = true;
        navMeshAgent.destination = target.transform.position;
    }


    public void ReturnToBase()
    {
        isArrived = false;
        navMeshAgent.enabled = false;
        animator.SetBool("IsRunning", false);
        transform.position = initialPosition;
    }
}
