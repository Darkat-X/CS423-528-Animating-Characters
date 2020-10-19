using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaScript : MonoBehaviour
{
    Animator anim;
    public float RunParam = 0;
    public float z_vel;
    public float timeScale;


    void Start ()
    {
        anim = GetComponent<Animator>();
    }



    void Update ()
    {
        Time.timeScale = timeScale;

        float x_vel = Input.GetAxis("Horizontal");
        float y_vel = Input.GetAxis("Vertical");
        if(Input.GetKey("left shift"))
        {
            RunParam = Mathf.Clamp01(RunParam + 1 * Time.deltaTime);
        }
        else
        {
            RunParam = Mathf.Clamp01(RunParam - 1 * Time.deltaTime);
        }
        if(Input.GetKeyDown("space"))
        {
            anim.SetTrigger("Jump");
        }



        anim.SetFloat ("x_vel",x_vel);
        anim.SetFloat ("y_vel",y_vel);
        anim.SetFloat ("Run",RunParam);
    }

}



