using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class MoveTowardTarget : MonoBehaviour
{

    private WaypointProgressTracker _waypointProgressTracker = null;

    public GameObject fleeTarget = null;

    public float fleeDistance = 1.0f;

    public bool fleeAlert = false;

    public float speed = 5.0f;

    void Awake()
    {
        _waypointProgressTracker = GetComponent<WaypointProgressTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fleeAlert) {
            var direction = _waypointProgressTracker.target.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }
        if (Vector3.Distance(transform.position, fleeTarget.transform.position) < fleeDistance && !fleeAlert) {
            fleeAlert = true;
            StartCoroutine(Flee());
        }
    }

    IEnumerator Flee()
    {
        yield return new WaitForSeconds(5.0f);
        fleeAlert = false;
    }

}
