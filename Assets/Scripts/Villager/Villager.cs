using UnityEngine;
using UnityEngine.AI;

public class Villager : MonoBehaviour
{
    public float roamingRadius = 10f;
    public float speed = 2f;
    private NavMeshAgent agent;
    private Vector3 originalPosition;
    private bool isPlayerNearby = false;
    private GameObject _player;
    public float interactionDistance = 2.5f;

    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        originalPosition = transform.position;
        MoveToRandomPosition();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);
        if (distance <= interactionDistance)
        {
            StopMovement();
        }
        else
        {
            ResumeMovement();
        }

        if (!isPlayerNearby && !agent.pathPending && agent.remainingDistance < 0.5f)
        {
            MoveToRandomPosition();
        }
    }

    void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * roamingRadius;
        randomDirection += originalPosition;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, roamingRadius, NavMesh.AllAreas))
        {
            Vector3 finalPosition = hit.position;
            agent.SetDestination(finalPosition);
        }
    }

    public void StopMovement()
    {
        if (agent != null)
        {
            agent.isStopped = true;
            isPlayerNearby = true;
        }
    }

    public void ResumeMovement()
    {
        if (agent != null && agent.isStopped)
        {
            agent.isStopped = false;
            isPlayerNearby = false;
            MoveToRandomPosition();
        }
    }
}