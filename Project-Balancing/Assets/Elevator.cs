using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public List<Transform> waypoints;
    public float speed = 1f;
    int currentInd = 0;
    Vector3 currentTarget;

    private void Update()
    {
        if (transform.position == currentTarget)
        {
            NextWaypoint();
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
    }
}
