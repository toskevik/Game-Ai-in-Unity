using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCSteeringPatrol : MonoBehaviour
{
    [Header("Waypoints")]
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float arrivalDistance = 1.0f;
    [SerializeField] private GameObject waypointMarker;

    [Header("Steering Settings")]
    [SerializeField] private float movementSpeed = 3.5f;
    [SerializeField] private float turnSpeed = 2.5f; // Slightly higher for obstacle avoidance
    
    [Tooltip("If the NPC is facing more than this angle away from the target, it will slow down to turn.")]
    [SerializeField] private float tightTurnAngle = 45f;

    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Hand over control to the script
        agent.updatePosition = false;
        agent.updateRotation = false;

        if (waypoints.Count > 0)
        {
            var destination = waypoints[currentWaypointIndex].position;
            agent.SetDestination(destination);
            waypointMarker.transform.position = waypoints[currentWaypointIndex].position;
        }
    }

    void Update()
    {
        if (waypoints.Count == 0 || agent.pathPending) return;

        ApplySteeringAndMovement();
        CheckWaypointArrival();
    }

    private void ApplySteeringAndMovement()
    {
        // 1. Get direction to the next point on the NavMesh path
        Vector3 targetDirection = (agent.steeringTarget - transform.position).normalized;
        targetDirection.y = 0;

        if (targetDirection != Vector3.zero)
        {
            // 2. Smoothly rotate toward the path corner/waypoint
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // 3. Dynamic Speed: If we are facing the wrong way, slow down to avoid overshooting obstacles
        float angleToTarget = Vector3.Angle(transform.forward, targetDirection);
        float currentSpeed = (angleToTarget > tightTurnAngle) ? movementSpeed * 0.2f : movementSpeed;

        // 4. Move and Constrain: agent.Move ensures we stay on the NavMesh and don't hit obstacles
        Vector3 movement = transform.forward * currentSpeed * Time.deltaTime;
        agent.Move(movement);

        // 5. Explicitly snap the transform to the valid NavMesh position
        transform.position = agent.nextPosition;
    }

    private void CheckWaypointArrival()
    {
        if (agent.remainingDistance <= arrivalDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
            var destination = waypoints[currentWaypointIndex].position;
            agent.SetDestination(destination);
            waypointMarker.transform.position = waypoints[currentWaypointIndex].position;
        }
    }
}