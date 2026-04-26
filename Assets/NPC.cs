using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public NavMeshAgent agent;
    public float range;
    public Transform centrePoint;

    private float stuckTimer = 0f;
    private float stuckThreshold = 2f; 
    private Vector3 lastPosition;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

       
        agent.avoidancePriority = Random.Range(10, 90);

        lastPosition = transform.position;
    }

    void Update()
    {
        DetectStuck();

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
            }
        }
    }

    void DetectStuck()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);

        if (distanceMoved < 0.05f) 
        {
            stuckTimer += Time.deltaTime;
        }
        else
        {
            stuckTimer = 0f;
        }

        lastPosition = transform.position;

        if (stuckTimer >= stuckThreshold)
        {
            StartCoroutine(RecoverFromStuck());
            stuckTimer = 0f;
        }
    }

    IEnumerator RecoverFromStuck()
    {
        agent.isStopped = true;

        // NPC "colide" por alguns segundos
        yield return new WaitForSeconds(2f);

        agent.isStopped = false;

        Vector3 point;
        if (RandomPoint(centrePoint.position, range, out point))
        {
            agent.SetDestination(point);
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}