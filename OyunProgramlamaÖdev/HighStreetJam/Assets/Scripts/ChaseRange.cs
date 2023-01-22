using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseRange : MonoBehaviour
{
    Transform target;
    EnemyDroper droper;
    PlayerController playerController;
    SpriteRenderer angrySprite;

    [SerializeField] public float chaseRange = 4f;
    public float chasingSpeed;
    public float walkSpeed;
    public string policeRunningSpeedUpgradeID = "policeRunningSpeedUpgradeID";


    NavMeshAgent navmeshAgent;
    float distanceToTarget = Mathf.Infinity;
    [HideInInspector] public bool isChasing = false;

    float t = 0;
    public float lastT = -3;

    private void Start()
    {
        chasingSpeed = PlayerPrefs.GetFloat(policeRunningSpeedUpgradeID, chasingSpeed);
        walkSpeed = chasingSpeed / 2f;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        navmeshAgent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").gameObject.GetComponent<Transform>();
        droper = gameObject.GetComponent<EnemyDroper>();
        angrySprite = transform.Find("Angry").GetComponent<SpriteRenderer>();
    }


    void Update()
    {

        distanceToTarget = Vector3.Distance(target.position, transform.position);

        t = Time.time - lastT;

        if (t > 7)
        {
            if (distanceToTarget <= chaseRange && !playerController.isItOn)
            {
                isChasing = true;
                navmeshAgent.speed = chasingSpeed;
                angrySprite.enabled = true;
            }
            else
            {
                navmeshAgent.speed = walkSpeed;
                isChasing = false;
                angrySprite.enabled = false;
            }
        }
        else
        {
            navmeshAgent.speed = walkSpeed;
            isChasing = false;
            angrySprite.enabled = false;
        }
        
    }



}
