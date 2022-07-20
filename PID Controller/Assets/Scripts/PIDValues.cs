using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PIDValues")]
public class PIDValues : ScriptableObject
{
    [Header("X Axis")]
    [Range(-10,10)]
    public float _xP;
    [Range(-10,10)]
    public float _xI, _xD;

    [Header("Y Axis")]
    [Range(-10,10)]
    public float _yP;
    [Range(-10,10)]
    public float _yI, _yD;

    [Header("Z Axis")]
    [Range(-10,10)]
    public float _zP;
    [Range(-10,10)]
    public float _zI, _zD;
}
