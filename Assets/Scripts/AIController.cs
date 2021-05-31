
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private Vector3 initialPosition;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private bool isArrived;

    private int score;
    public TextMeshProUGUI scoreText;

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

        score = 0;
        scoreText.enabled = true;
        SetScoreText();
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

    private void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;

            SetScoreText();
        }
    }
}
