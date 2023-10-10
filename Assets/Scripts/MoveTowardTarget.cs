using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class MoveTowardTarget : MonoBehaviour
{

    private WaypointProgressTracker _waypointProgressTracker = null;

    public GameObject fleeTarget = null;

    public float fleeDistance = 5.0f;

    public float speed = 5.0f;

    void Awake()
    {
        _waypointProgressTracker = GetComponent<WaypointProgressTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, fleeTarget.transform.position) < fleeDistance)
        {
            var direction = _waypointProgressTracker.target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
    }


}
