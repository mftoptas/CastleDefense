using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] //Makes instances of a script always execute, both as part of Play Mode and when editing.

public class CoordinateLabeler : MonoBehaviour
{
    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();

    void Awake() // Awake method gets called before start.
    {
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates(); // Now coordinates looks after game starts as well.
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x); // I use UnityEditor.EditorSnapSettings.move.x here because i make grid's scale 10 but i want coordinates not to be multipy of 10.
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z); // My 2D axis are x and z so i use z here not y.

        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
