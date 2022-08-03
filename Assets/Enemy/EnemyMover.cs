using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)]float speed = 1f; // Enemy speed coefficient.

    void Start()
    {
        StartCoroutine(FollowPath()); // The execution of a coroutine can be paused at any point using the yield statement. When a yield statement is used, the coroutine pauses execution and automatically resumes at the next frame.
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path) // Get started by coroutine.
        {
            Vector3 startPosition = transform.position;
            Vector3 endPosition = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPosition); // I am always going to be facing the waypoint that we're heading towards.

            while(travelPercent < 1f) // Means while i am not at the end position.
            {
                travelPercent += Time.deltaTime * speed;
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent); // Lerp: Linearly interpolates between two points.
                yield return new WaitForEndOfFrame();
            }
        }
    }
}