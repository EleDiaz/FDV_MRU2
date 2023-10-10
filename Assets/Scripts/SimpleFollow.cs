using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFollow : MonoBehaviour
{

    public GameObject target = null;

    public float speed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var targetPosition = target.transform.position;
        transform.LookAt(new Vector3(targetPosition.x, transform.position.y, targetPosition.z));
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

}
