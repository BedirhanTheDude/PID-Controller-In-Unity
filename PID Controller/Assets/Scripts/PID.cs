using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PID
{
    private float _p, _i, _d;
    private float _kp, _ki, _kd;
    private float _lastError;

    public float Kp {get{ return _kp; } set{ _kp = value ; }} //Proportional
    public float Ki {get{ return _ki; } set{ _ki = value ; }} //Integral
    public float Kd {get{ return _kd; } set{ _kd = value ; }} //Derivative

    public PID (float p, float i, float d)
    {
        _kp = p;
        _ki = i;
        _kd = d;
    }

    public float Output(float Error, float dt) {
        
        _p = Error;
        _i += _p * dt;
        _d = (_p - _lastError) / dt;
        _lastError = Error;
        float output = _p * Kp + _i * Ki + _d * Kd;

        return output;
    }

}
