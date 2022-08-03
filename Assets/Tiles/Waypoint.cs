using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] GameObject towerPrefab;
    [SerializeField] bool isPlaceable;

    void OnMouseDown() // OnMouseDown is called when the user has pressed the mouse button while over the Collider.
    {
        if (isPlaceable)
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity); // Instantiate: Clones the object original and returns the clone.
            isPlaceable = false; // Make tile not placeable anymore adter adding tower, bacause it already has a tower on it.
        }
    }
}