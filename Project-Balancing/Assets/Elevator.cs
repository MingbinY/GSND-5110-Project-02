using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 1f;
    public float waitTime = 2f;
    int currentInd = 0;
    bool changingWaypoint=false;
    Vector3 currentTarget;

    private void Awake()
    {
        currentInd = 0;
        currentTarget = waypoints[currentInd].position;
    }

    private void Update()
    {
        if (transform.position == currentTarget && !changingWaypoint)
        {
            changingWaypoint = true;
            Invoke("NextWaypoint", waitTime);
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime); 
    }

    void NextWaypoint()
    {
        if (currentInd == waypoints.Count - 1)
        {
            currentInd = 0;
        }
        else
        {
            currentInd++;
        }

        currentTarget = waypoints[currentInd].position;
        changingWaypoint = false;
    }
}
