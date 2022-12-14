using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] //Makes instances of a script always execute, both as part of Play Mode and when editing.
[RequireComponent(typeof(TextMeshPro))]

public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] Color defaultColor = Color.white;
    [SerializeField] Color blockedColor = Color.gray;

    TextMeshPro label;
    Vector2Int coordinates = new Vector2Int();
    Waypoint waypoint;

    void Awake() // Awake method gets called before start.
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        waypoint = GetComponentInParent<Waypoint>(); // Used GetComponentInParent because the waypoint is on the route of our object and our coordinate label is buried in there in one of the children.
        DisplayCoordinates(); // Now coordinates looks after game starts as well.
    }

    void Update()
    {
        if(!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true;
        }

        SetLabelColor();
        ToggleLabels();
    }

    void ToggleLabels()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive(); // If C key is pressed labels are not going to appear.
        }
    }

    void SetLabelColor()
    {
        if(waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
        }
    }

    void DisplayCoordinates()
    {
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x); // I use UnityEditor.EditorSnapSettings.move.x here because i make grid's scale 10 but i want coordinates not to be multipy of 10.
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z); // My 2D axis are x and z so i use z here not y.
        // Unity Editor can't be built into our final project. So i create a folder named Editor. What this folder does is gets ignored when we try and build our project.
        // So when it comes time to build, all we need to do is drag CoordinateLabeler up over into this folder and it won't be included in our final build.
        // However, scripts in this folder can also not be added to objects in our scene. So if we were to drag it over now, it would break the CoordinateLabeler for our editing purposes.
        // So for now, leave this script where it is. But when you come to build your project, just drag that over into the editor folder and then that will remove any build errors that you might have.
        label.text = coordinates.x + "," + coordinates.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = coordinates.ToString();
    }
}