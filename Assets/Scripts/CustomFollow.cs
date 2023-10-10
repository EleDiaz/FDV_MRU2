using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CustomFollow : MonoBehaviour
{

    public GameObject[] gameObjects;

    public float reachDistance = 0.5f;

    public float speed = 5.0f;

    // Rotation isnt fixed due to slerp been used as a rotation driver.
    // So, the speed depends on angle to be rotate bigger angles will rotate faster.
    // Value of 1 will not affect the rotation.
    public float rotationSpeed = 5.0f;

    public bool likeACar = false;

    private int _objectToFollow = 0;

    private Rigidbody _rigidbody = null;

    void Awake()
    {
        try {
            _rigidbody = GetComponent<Rigidbody>();
        }
        catch (System.Exception e) {
            Debug.LogError("Rigidbody not found");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (gameObjects.Length == 0)
        {
            Debug.LogError("No game objects to follow");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody != null) return;

        reachedTarget();

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimedDirection()), rotationSpeed * Time.deltaTime);

        transform.Translate(aimedDirection() * speed * Time.deltaTime, Space.World);
    }

    // Going throught the physics engine to follow the objects
    void FixedUpdate()
    {
        if (_rigidbody == null) return;

        reachedTarget();
        _rigidbody.MoveRotation(
            Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(aimedDirection()), rotationSpeed * Time.fixedDeltaTime
            )
        );

        if (likeACar) {
            _rigidbody.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        }
        else {
            _rigidbody.MovePosition(aimedDirection() * speed * Time.fixedDeltaTime);
        }
    }

    void NextGameObject()
    {
        _objectToFollow = (_objectToFollow + 1) % gameObjects.Length;
    }

    Vector3 aimedDirection()
    {
        var direction = (gameObjects[_objectToFollow].transform.position - transform.position).normalized;
        return new Vector3(direction.x, 0, direction.z);
    }

    void reachedTarget() {
        var targetPos = new Vector3(gameObjects[_objectToFollow].transform.position.x, transform.position.y, gameObjects[_objectToFollow].transform.position.z);

        var distance = Vector3.Distance(targetPos, transform.position);

        if (distance <= reachDistance)
        {
            NextGameObject();
            return;
        }
    }
}
