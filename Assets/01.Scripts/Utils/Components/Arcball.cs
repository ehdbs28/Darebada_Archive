using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arcball
{
    private float _radius;
    public float Radius => _radius;

    private Transform _target;
    public Vector3 Center => _target.position;

    public Arcball(float radius, Transform target){
        _radius = radius;
        _target = target;
    }

    public Vector3 GetSphericalCoordinates(Vector3 cartesian)
    {
        float phi = Mathf.Atan2(cartesian.y, cartesian.x);
        float theta = Mathf.Acos(cartesian.z / _radius);

        if (cartesian.x < 0)
            phi += Mathf.PI;

        return new Vector3 (_radius, phi, theta);
    }

    public Vector3 GetCartesianCoordinates(Vector3 spherical)
    {
        Vector3 ret = new Vector3 ();

        ret.x = spherical.x * Mathf.Cos (spherical.z) * Mathf.Cos (spherical.y);
        ret.y = spherical.x * Mathf.Sin (spherical.z);
        ret.z = spherical.x * Mathf.Cos (spherical.z) * Mathf.Sin (spherical.y);

        return ret;
    }
}
