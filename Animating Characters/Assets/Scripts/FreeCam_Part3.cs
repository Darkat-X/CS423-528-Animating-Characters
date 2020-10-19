using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class FreeCam_Part3 : MonoBehaviour
{
    public float speed = 30f;
    public float zoomSensitivity = 10f;

    void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position + (-transform.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + (transform.right * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + (transform.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + (-transform.up * speed * Time.deltaTime);
        }

        float axis = Input.GetAxis("Mouse ScrollWheel");
        if (axis != 0)
        {
            transform.position = transform.position + transform.forward * axis * zoomSensitivity;
        }

 }

}
