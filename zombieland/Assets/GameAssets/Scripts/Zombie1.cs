using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie1 : MonoBehaviour
{
    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform lookpoint;
    public LayerMask playerLayer;

    [Header("Zombie guarding var")]
    public GameObject[] walkpoints;
    int currentZombiePosition = 0;
    public float ZombieSpeed;
    float walkingInRadius = 2;

    [Header("zombie state")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInVisionRadius;
    public bool PlayerInattackingRadius;

    private void Awake()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius,playerLayer);
        PlayerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius,playerLayer);

        if(playerInVisionRadius && !PlayerInattackingRadius )
        {
            Guard();
        }
    }

    void Guard()
    {
        if (Vector3.Distance(walkpoints[currentZombiePosition].transform.position, transform.position)< walkingInRadius)
        {
            currentZombiePosition = Random.Range(0,walkpoints.Length);
            if(currentZombiePosition>=walkpoints.Length)
            {
                currentZombiePosition = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkpoints[currentZombiePosition].transform.position, Time.deltaTime * ZombieSpeed);
    }






}
