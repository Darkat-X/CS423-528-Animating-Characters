using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

public class FreeCam : MonoBehaviour
{
    public float speed = 30f;
    public float zoomSensitivity = 10f;

    private HashSet<GameObject> selected;
    private string selectedTag;
    private string[] selectableTags = new[] {"Eva"};

    void Start()
    {
        selected = new HashSet<GameObject>();

    }

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

//-------------------------------------------------------------------------------------------------------

        if(Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        
            if (Physics.Raycast(ray, out hit)) 
            {
                var objectHit = hit.transform.gameObject;

                if(selectableTags.Contains(objectHit.tag))
                {
                    print(objectHit.name);
                    if(objectHit.tag == selectedTag || selected.Count == 0)
                    {
                        if(selected.Contains(objectHit.gameObject))
                        {
                            selected.Remove(objectHit.gameObject);
                            if(objectHit.tag == "Eva")
                            {
                                objectHit.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
                            }
                        }
                        else
                        {
                            selected.Add(objectHit.gameObject);
                        }
                        selectedTag = objectHit.tag;
                    }
                    else
                    {
                        selected.Clear();
                        selected.Add(objectHit);
                        selectedTag = objectHit.tag;
                    }
                }
                else
                {
                    if(selectedTag == "Eva")
                    {
                        // foreach(var agent in selected)
                        // {
                        //     agent.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(hit.point);
                        // }
                    }
                }

                if(selectedTag == "Eva")
                {
                    // foreach(var agent in selected)
                    // {
                    //     agent.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(hit.point);
                    // }
                }

                print(selected);
                foreach(GameObject a in selected)
                {
                    print(a);
                }
            }

        }

        if(Input.GetMouseButtonDown(0) && selected != null)
                {
                    RaycastHit hit;
                    Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                        if (Physics.Raycast(ray, out hit)) {
                            foreach(var agent in selected)
                            {
                                agent.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(hit.point);
                            }
                        }
                }

        foreach(var go in selected)
        {
            Debug.DrawLine(go.transform.position, go.transform.position + Vector3.up * 3, Color.red);
        }

//-------------------------------------------------------------------------------------------------------
    }

}
