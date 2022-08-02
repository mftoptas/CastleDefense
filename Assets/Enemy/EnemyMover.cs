using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] float waitTime = 1f; // Time delay

    void Start()
    {
        StartCoroutine(FollowPath()); // The execution of a coroutine can be paused at any point using the yield statement. When a yield statement is used, the coroutine pauses execution and automatically resumes at the next frame.
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(waitTime); // By default, Unity resumes a coroutine on the frame after a yield statement. If you want to introduce a time delay, use WaitForSeconds:
        }
    }
}