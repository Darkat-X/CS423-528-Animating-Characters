using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaNavScript : MonoBehaviour
{
    Animator anim;
    public Transform target;

    void Start ()
    {
        anim = GetComponent<Animator>();
    }


    void Update ()
    {
        var direction = transform.forward;
        var desiredDirection = target.position - transform.position;
        desiredDirection.y = 0;
        var distance = desiredDirection.magnitude;
        desiredDirection.Normalize();
        var angularDeviation = Vector3.SignedAngle(direction, desiredDirection, Vector3.up);

        var angDesiredSpeed = 0.5f;
        var forDesiredSpeed = 0.5f;

        float x_vel = 0;
        float y_vel = 0;

        if (Mathf.Abs(angularDeviation) < 45)
        {
            y_vel = Mathf.Min(forDesiredSpeed, distance);
        }
        if (Mathf.Abs(angularDeviation) > 5)
        {
            x_vel = Mathf.Min(Mathf.Abs(angularDeviation / 180f), angDesiredSpeed) * Mathf.Sign(angularDeviation);
        }
        

        anim.SetFloat ("x_vel",x_vel);
        anim.SetFloat ("y_vel",y_vel);
    }
}

