using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    //PID Instances for each axis
    PID _xPID;
    PID _yPID;
    PID _zPID;

    [SerializeField] private Transform targetTransform;
    [SerializeField] private PIDValues values;

    private float _xP, _xI, _xD;
    private float _yP, _yI, _yD;
    private float _zP, _zI, _zD;

    [SerializeField] private float maxAngVel = 20f;
    
    private Rigidbody _rb;

    void Awake()
    {
        _rb = this.GetComponent<Rigidbody>();
        _rb.maxAngularVelocity = maxAngVel;
        _xPID = new PID( _xP , _xI , _xD );
        _yPID = new PID( _yP , _yI , _yD );
        _zPID = new PID( _zP , _zI , _zD );
    }
    
    void Update()
    {
        (_xP, _xI, _xD) = (values._xP, values._xI, values._xD);
        (_yP, _yI, _yD) = (values._yP, values._yI, values._yD);
        (_zP, _zI, _zD) = (values._zP, values._zI, values._zD);
        
        //Proportional, Integral and Derivative values for each axis' PIDs
        _xPID.Kp = _xP;
        _xPID.Ki = _xI;
        _xPID.Kd = _xD;

        _yPID.Kp = _yP;
        _yPID.Ki = _yI;
        _yPID.Kd = _yD;

        _zPID.Kp = _zP;
        _zPID.Ki = _zI;
        _zPID.Kd = _zD;
    }
    
    void FixedUpdate()
    {
        Vector3 targetDirection = transform.position - targetTransform.position;
        Vector3 rotationVector = Vector3.RotateTowards(transform.TransformDirection(transform.up), targetDirection, 360 , 0f);
        Quaternion targetQuaternion = Quaternion.LookRotation(rotationVector);

        float xError = Mathf.DeltaAngle(transform.eulerAngles.x, targetQuaternion.eulerAngles.x);
        float xTorque = _xPID.Output(xError, Time.fixedDeltaTime);
        float yError = Mathf.DeltaAngle(transform.eulerAngles.y, targetQuaternion.eulerAngles.y);
        float yTorque = _yPID.Output(yError, Time.fixedDeltaTime);
        float zError = Mathf.DeltaAngle(transform.eulerAngles.z, targetQuaternion.eulerAngles.z);
        float zTorque = _zPID.Output(zError, Time.fixedDeltaTime);

        _rb.AddRelativeTorque( (xTorque * Vector3.right) + (yTorque * Vector3.up) + (zTorque * Vector3.forward));
        _rb.AddRelativeForce(-10 * Vector3.forward);
    }
}
