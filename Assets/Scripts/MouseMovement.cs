using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MouseMovement : MonoBehaviour
{
    private NavMeshAgent mouseAgent;
    public float range = 10.0f;
    public float timerMax = 5.0f;
    private float timer = 0f;
    public bool canMove = true;

    public bool isSearching = true;
    public Transform target;
    public Transform home;


    private void Start()
    {
        mouseAgent = gameObject.GetComponent<NavMeshAgent>();
        timer = timerMax;
    }


    bool RandomPoint(Vector3 center, float range, out Vector3 result) // gets a randoim point on the navmesh
    {
        for (int i = 0; i < 30; i++)
        {

            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.GetAreaFromName("PreyArea")))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void Update()
    {
        if (canMove)
        {
            if (isSearching)
            {
                mouseAgent.SetDestination(target.position);
                if(Vector3.Distance(transform.position, target.position) <= 1f)
                {
                    isSearching = false;
                }
            }else
            {
                mouseAgent.SetDestination(home.position);
                
                    if (Vector3.Distance(transform.position, home.position) <= 1f)
                    {
                        isSearching = true;
                    }
                
            }
            // timer += Time.deltaTime;
            // if (timer >= timerMax)
            // {
            //     Vector3 point;
            //     if (RandomPoint(transform.position, range, out point))
            //     {
            //         mouseAgent.SetDestination(point);
            //     }
            //     timer = 0f;
            // }


        }
    }


}
