using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class NpcController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float arrivalDistance = 0.5f;
    
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Ensure the agent handles the rotation to face movement direction
        agent.updateRotation = true;
        agent.angularSpeed = 120f; // Adjust for turn speed

        if (waypoints.Count > 0)
        {
            SetDestinationToWaypoint();
        }
        else
        {
            Debug.LogWarning("No waypoints assigned to the NPC.");
        }
    }

    void Update()
    {
        if (waypoints.Count == 0) return;

        // Check if the agent has reached the current waypoint
        if (!agent.pathPending && agent.remainingDistance <= arrivalDistance)
        {
            IterateWaypointIndex();
            SetDestinationToWaypoint();
        }
    }

    private void SetDestinationToWaypoint()
    {
        agent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    private void IterateWaypointIndex()
    {
        // Simple sequential loop: 0, 1, 2, 0, 1...
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
    }
}
