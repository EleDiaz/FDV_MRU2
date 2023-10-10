using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody _rigidbody = null;

    public InputActionReference movementAction = null;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        if (_rigidbody == null) return;

        var movement = movementAction.action.ReadValue<Vector2>();

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

}
