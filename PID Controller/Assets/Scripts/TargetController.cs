using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private Rigidbody _rb;
    private bool _upDownDefined;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _upDownDefined = false;
        try
        {
            Input.GetAxis("UpDown");
        }
        catch
        {
            Debug.LogError("The UpDown axis is not defined, " +
                           "please define it using the input manager before running the application.");
        }
        _upDownDefined = true;
    }

    private void MoveTarget()
    {
        float forwardAxis = -Input.GetAxis("Vertical");
        float rightAxis = -Input.GetAxis("Horizontal");
        float upAxis = Input.GetAxis("UpDown");

        transform.position += transform.forward * forwardAxis * moveSpeed * Time.fixedDeltaTime;
        transform.position += transform.right * rightAxis * moveSpeed * Time.fixedDeltaTime;
        transform.position += transform.up * upAxis * moveSpeed * Time.fixedDeltaTime;

    }

    private void RotateTarget()
    {
        float rotationAxis = 0;
        if (Input.GetKey(KeyCode.E))
        {
            rotationAxis = 1;
        }
        else if (Input.GetKey(KeyCode.Q))
        {
            rotationAxis = -1;
        }
        else if (!Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
            rotationAxis = 0;

        transform.Rotate(new Vector3(0, rotationAxis * rotationSpeed * Time.fixedDeltaTime, 0));
    }

    private void FixedUpdate()
    {
        MoveTarget();
        RotateTarget();
        _rb.velocity = Vector3.zero;
    }
}
