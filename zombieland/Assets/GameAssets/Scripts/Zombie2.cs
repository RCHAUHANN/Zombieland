using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie2 : MonoBehaviour
{
    [Header("Zombie Health and Damamge")]
    public float giveDamage = 5f;
    private float zombieHealth = 100f;
    private float presentHealth;
    public HealthBar healthBar;

    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform lookpoint;
    public Camera AttackRaycastArea;
    public Transform playerbody;
    public LayerMask playerLayer;
    public float rotationSpeed = 2f;

    [Header("Zombie Standing var")]
    public float ZombieSpeed;
    

    [Header("Zombie Attack Var")]
    public float TimeBtwAttack;
    bool previouslyAttack;

    [Header("zombie state")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInVisionRadius;
    public bool PlayerInattackingRadius;

    [Header("Zombie animation")]
    public Animator animator;

    private void Awake()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
        presentHealth = zombieHealth;
        healthBar.giveFullHealth(zombieHealth);
    }

    private void Update()
    {
        playerInVisionRadius = Physics.CheckSphere(transform.position, visionRadius, playerLayer);
        PlayerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, playerLayer);

        if (!playerInVisionRadius && !PlayerInattackingRadius)
        {
            Stand();
        }
        if (playerInVisionRadius && !PlayerInattackingRadius)
        {
            PursuePlayer();
        }
        if (playerInVisionRadius && PlayerInattackingRadius)
        {
            AttackPlayer();
        }
    }

    void Stand()
    {
       zombieAgent.SetDestination(transform.position);
        animator.SetBool("idle", true);
        animator.SetBool("running", false);
    }

    void PursuePlayer()
    {
        if (zombieAgent.SetDestination(playerbody.position))
        {
            animator.SetBool("idle", false);
            animator.SetBool("running", true);
            animator.SetBool("attacking", false);
            
        }
        

    }
    void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        Vector3 lookDir = (lookpoint.position - transform.position).normalized;
        lookDir.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        if (!previouslyAttack)
        {
            RaycastHit hitinfo;
            if (Physics.Raycast(AttackRaycastArea.transform.position, AttackRaycastArea.transform.forward, out hitinfo, attackingRadius,playerLayer))

            {
                Debug.Log("attacking...." + hitinfo.transform.name);
                PlayerScript playerbody = hitinfo.transform.GetComponent<PlayerScript>();
                if (playerbody != null)
                {
                    playerbody.playerHitDamage(giveDamage);
                }
                
                animator.SetBool("running", false);
                animator.SetBool("attacking", true);
               
            }
            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), TimeBtwAttack);

        }

    }

    void ActiveAttacking()
    {
        previouslyAttack = false;
    }
    public void zombieHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        healthBar.SetHealth(presentHealth);
        if (presentHealth <= 0)
        {
            
            animator.SetBool("died", true);
            zombieDie();
        }
    }
    private void zombieDie()
    {
        zombieAgent.SetDestination(transform.position);
        ZombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        PlayerInattackingRadius = false;
        playerInVisionRadius = false;
        Object.Destroy(gameObject, 5.0f);
    }
}
