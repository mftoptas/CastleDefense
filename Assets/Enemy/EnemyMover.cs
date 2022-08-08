using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    [SerializeField] [Range(0f, 5f)] float speed = 1f; // Enemy speed coefficient.

    void Start()
    {
        FindPath();
        ReturnStart();
        StartCoroutine(FollowPath()); // The execution of a coroutine can be paused at any point using the yield statement. When a yield statement is used, the coroutine pauses execution and automatically resumes at the next frame.
    }

    void FindPath()
    {
        path.Clear(); // So when we find a path, we're going to clear the existing one and then add a new one.

        GameObject[] waypoints = GameObject.FindGameObjectsWithTag("Path"); // And this will now find all of the objects tagged as a Path and place it into an array.

        foreach(GameObject waypoint in waypoints)
        {
            path.Add(waypoint.GetComponent<Waypoint>()); // Find the waypoint component on that object and then add that to our list called Path.
        }
    }

    void ReturnStart()
    {
        transform.position = path[0].transform.position; // It's going to move our enemy into the first waypoint.
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
        Destroy(gameObject);
    }
}