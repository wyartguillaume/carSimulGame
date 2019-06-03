using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPointManager : MonoBehaviour { 

    private NavMeshAgent navMeshAgent;

    public Transform[] patrolPoints;

    private int currentControlPointIndex = 0;


    protected void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        MoveToNextPatrolPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.enabled)
        {

            bool patrol = true;
            // Patrolling between points if there are patrol points
            if (patrol)
            {
                if (!navMeshAgent.pathPending && 
                    navMeshAgent.remainingDistance < 0.5f)
                    MoveToNextPatrolPoint();
            }

        }
    }

    void MoveToNextPatrolPoint()
    {
        if (patrolPoints.Length >= 0)
        {
            navMeshAgent.destination = patrolPoints[currentControlPointIndex].position;
            currentControlPointIndex++;
            currentControlPointIndex %= patrolPoints.Length;
        }
    }

}
