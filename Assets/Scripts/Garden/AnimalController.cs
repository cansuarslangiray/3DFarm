using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum AnimalState
{
    Idle,
    Strolling
}

public class AnimalController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;

    [Header("Stroll")] public float strollSpeed = 3f;
    public float strollTime = 5.0f;
    public float strollDist = 1f;

    [Header("Idle")] public float idleSpeed = 0f;
    public float idleTime = 3.0f;

    public AnimalState currState = AnimalState.Idle; //at beginning -> idle

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = idleSpeed;
        currState = AnimalState.Idle;

        ChangeState();
    }

    private void Update()
    {
        if (gameObject.GetComponent<Animal>().isReady)
        {
            navMeshAgent.isStopped = true;
        }

        float speed = currState == AnimalState.Idle ? idleSpeed : strollSpeed;
        navMeshAgent.speed = speed;
    }

    public void ChangeState()
    {
        switch (currState)
        {
            case AnimalState.Idle:
                HandleIdleState();
                break;
            case AnimalState.Strolling:
                HandleStrollingState();
                break;
        }
    }

    public Vector3 RandomPosition(Vector3 origin, float distance)
    {
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navMeshHit;

        if (NavMesh.SamplePosition(randomDirection, out navMeshHit, distance, NavMesh.AllAreas))
        {
            return navMeshHit.position;
        }
        else
        {
            return RandomPosition(origin, distance);
        }
    }

    private void HandleIdleState()
    {
        StartCoroutine(WaitToMove());
    }

    private IEnumerator WaitToMove()
    {
        float waitTime = Random.Range(idleTime / 2, idleTime * 2);
        yield return new WaitForSeconds(waitTime);

        Vector3 randomDestination = RandomPosition(transform.position, strollDist);

        navMeshAgent.SetDestination(randomDestination);
        //Debug.Log("corrrect?" + randomDestination);
        SetState(AnimalState.Strolling);
    }

    private void HandleStrollingState()
    {
        StartCoroutine(WaitToReachDestination());
    }

    private IEnumerator WaitToReachDestination()
    {
        float startTme = Time.time;

        while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            if (Time.time - startTme >= strollTime)
            {
                navMeshAgent.ResetPath();
                SetState(AnimalState.Idle);
                yield break;
            }

            yield return null;
        }

        SetState(AnimalState.Idle);
        //
    }

    public void SetState(AnimalState newState)
    {
        if (currState == newState)
        {
            return;
        }

        currState = newState;
        OnStateChanged(newState);
    }

    private void OnStateChanged(AnimalState newState)
    {
        ChangeState();
    }
}