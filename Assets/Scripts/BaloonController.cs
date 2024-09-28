using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonController : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2.0f;

    int waypointIndex = 0;
    void Start()
    {

    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (waypointIndex < waypoints.Length)
        {
            // Move the balloon towards the next waypoint
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, speed * Time.deltaTime);

            // Check if the balloon has reached the current waypoint
            if (Vector2.Distance(transform.position, waypoints[waypointIndex].position) < 0.1f)
            {
                waypointIndex++; // Move to the next waypoint
            }
        }
        else
        {
            Destroy(gameObject); // Balloon reaches the end of the path
        }
    }

    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
    }
}