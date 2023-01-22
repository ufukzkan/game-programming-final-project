using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoliceAI : MonoBehaviour
{



    NavMeshAgent agent;
    ChaseRange range;
    Animator animator;
    public Transform low;
    public Transform high;
    public Transform target;
    private GameObject player;
    private NavMeshPath path;


    Vector3 newTarget;
    string name2;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        range = gameObject.GetComponent<ChaseRange>();
        animator = gameObject.transform.Find("PoliceMesh").GetComponent<Animator>();
        player = GameObject.Find("Player");
        target = transform;
        string name1 = gameObject.name.ToString();

        int counter = 0;
        foreach (char char1 in name1)
        {
            counter++;
            if(counter>6)
                name2 += Char.ToString(char1);
        }
        

        low = GameObject.Find("low" + name2).GetComponent<Transform>();
        high = GameObject.Find("high" + name2).GetComponent<Transform>();
        target = GameObject.Find("target" + name2).GetComponent<Transform>();

        newTarget = target.position;
        

        path = new NavMeshPath();
        RandomWaypoint();

    }

    void Update()
    {
        if (range.isChasing)
        {
            animator.SetBool("isRunning", true);
            while(!NavMesh.CalculatePath(agent.gameObject.transform.position, player.transform.position, NavMesh.AllAreas, path)) { RandomWaypoint(); }
            agent.SetPath(path);

        }
        else if (Vector3.Distance(transform.position, target.position) < 1)
        {
            RandomWaypoint();
        }
        else
        {
            while (!NavMesh.CalculatePath(agent.gameObject.transform.position, target.position, NavMesh.AllAreas, path)) { RandomWaypoint(); }
            agent.SetPath(path);
    
            animator.SetBool("isRunning", false);
        }

    }
    public void RandomWaypoint()
    {

        newTarget = new Vector3(UnityEngine.Random.Range(low.position.x, high.position.x),1f, UnityEngine.Random.Range(low.position.z, high.position.z));

        target.position = newTarget;
        
    }
}