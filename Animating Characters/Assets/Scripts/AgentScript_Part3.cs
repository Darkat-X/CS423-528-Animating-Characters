using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentScript_Part3 : MonoBehaviour
{
    public Transform wayfindingTarget;
    private NavMeshAgent navMeshAgent;
    private float RunParam;
    private bool touchEva;
    private bool JumpParam;
    Animator anim;
    AnimatorStateInfo animatorInfo;

    void Start()
    {
        print(wayfindingTarget);
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.SetDestination(wayfindingTarget.position);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, wayfindingTarget.position) > 1){
            navMeshAgent.SetDestination(navMeshAgent.destination);
            findDestination(navMeshAgent.destination);
        }

        if (((Mathf.Abs(navMeshAgent.destination.x - navMeshAgent.nextPosition.x) <= 2.05f) && (Mathf.Abs(navMeshAgent.destination.z - navMeshAgent.nextPosition.z) <= 2.05f)) 
        || ((Mathf.Abs(navMeshAgent.destination.x - navMeshAgent.nextPosition.x) <= 8.05f) && (Mathf.Abs(navMeshAgent.destination.z - navMeshAgent.nextPosition.z) <= 8.05f) && (touchEva == true))
        )

        {
            navMeshAgent.ResetPath();
        }

        if (navMeshAgent.isOnOffMeshLink)
        {
            JumpParam = true;
            anim.SetTrigger("Jump");
            anim.SetBool("JumpParam",JumpParam);
            //With this you can acces the start and the endpoint of the current offmeshlink
            OffMeshLinkData data = navMeshAgent.currentOffMeshLinkData;
 
            //calculate the final point of the link
            Vector3 endPos = data.endPos + Vector3.up * navMeshAgent.baseOffset;
 
            //Move the agent to the end point
            navMeshAgent.transform.position = Vector3.MoveTowards(navMeshAgent.transform.position, endPos, navMeshAgent.speed * Time.deltaTime);
 
            //when the agent reach the end point you should tell it, and the agent will "exit" the link and work normally after that
            if(navMeshAgent.transform.position == endPos)
            {
                navMeshAgent.CompleteOffMeshLink();
            }

            
        }



    }

    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Eva"))
        {
            touchEva = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Eva"))
        {
            touchEva = false;
        }
    }

    private void findDestination(Vector3 target){
        var direction = transform.forward;
        var desiredDirection = target - transform.position;
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
        if(Input.GetKey("left shift"))
        {
            RunParam = Mathf.Clamp01(RunParam + 1 * Time.deltaTime);
        }
        else
        {
            RunParam = Mathf.Clamp01(RunParam - 1 * Time.deltaTime);
        }
        

        anim.SetFloat ("x_vel",x_vel);
        anim.SetFloat ("y_vel",y_vel);
        anim.SetFloat ("Run",RunParam);
    }

}
