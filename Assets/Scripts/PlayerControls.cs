using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody _rigidbody = null;

    public InputActionReference movementAction = null;

    public float speed = 5f;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        movementAction.action.Enable();
    }


    void FixedUpdate()
    {
        if (_rigidbody == null) return;

        var movement = movementAction.action.ReadValue<Vector2>();

        _rigidbody.MovePosition(transform.position + new Vector3(movement.x, 0, movement.y) * speed * Time.fixedDeltaTime);
    }
}
