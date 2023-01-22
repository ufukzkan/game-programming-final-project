using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class SupportNavMesh : MonoBehaviour
{
    private Transform playerTransform;
    private GameObject player;
    private NavMeshAgent navMeshAgent;
    private BuyingSupport buyingScript;
    private Animator anim;
    private PlayerController playerController;
    private Animator animatorPlayer;
    private NavMeshPath path;

    public float speedCoefficient;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        buyingScript = GetComponent<BuyingSupport>();
        anim = transform.Find("DwarfIdle").gameObject.GetComponent<Animator>();
        playerController = player.GetComponent<PlayerController>();
        navMeshAgent.speed = playerController.speedRb * speedCoefficient;
        animatorPlayer = player.transform.Find("Mesh").gameObject.GetComponent<Animator>();
        path = new NavMeshPath();
    }

    private void Update()
    {
        navMeshAgent.angularSpeed = 800;
        //if its selled then its gonna follow the player
        if (buyingScript.isItSelled) { NavMesh.CalculatePath(navMeshAgent.gameObject.transform.position, playerTransform.position, NavMesh.AllAreas, path); navMeshAgent.SetPath(path); anim.SetBool("isSpraying", animatorPlayer.GetBool("IsSpraying")); }




        if (playerController.isItOn && (playerController.WhichWallTrigger != null && !playerController.WhichWallTrigger.didIEarn))
        {
            navMeshAgent.speed = 0;
            navMeshAgent.angularSpeed = 0;
        }
        else if (playerController.speedRb * speedCoefficient <= 0)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > 20)
                navMeshAgent.speed = 4.5f * 3;
            else
                navMeshAgent.speed = 4.5f;
        }
        else if(playerController.speedRb * speedCoefficient > 0)
        {
            if(Vector3.Distance(player.transform.position, transform.position)>20)
                navMeshAgent.speed = playerController.speedRb * speedCoefficient * 3;
            else
                navMeshAgent.speed = playerController.speedRb * speedCoefficient;
        }
        
      
        anim.SetFloat("speed", navMeshAgent.velocity.magnitude);
        
        
    }
}
