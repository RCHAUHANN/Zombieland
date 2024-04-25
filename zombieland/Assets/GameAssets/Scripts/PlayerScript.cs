using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;
    public float playerSprint = 3f;

    [Header("player health var")]
    private float playerHealth = 120f;
    public float presentHealth;
    public HealthBar playerHealthBar;

    [Header("Player Script Camera")]
    public Transform playerCamera;
    public GameObject endGameMenu;

    [Header("Player Animator and Gravity")]
    public CharacterController characterController;
    public float gravity = -9.81f;
    public Animator animator;

    [Header("Player Jumping and velocity")]
    public float turncalmTime = 0.1f;
    float turnCalmVelocity;
    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool Onsurface;
    public float surfaceDistance;
    public LayerMask surfaceMask;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        presentHealth =playerHealth;
        playerHealthBar.giveFullHealth(playerHealth);
    }



    private void Update()
    {
        Onsurface = Physics.CheckSphere(surfaceCheck.position, surfaceDistance,surfaceMask);
        if (Onsurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity *Time.deltaTime);
        playerMove();

        Jump();
        Sprint();
    }

    void playerMove()
    {
        float horizontal_axis = Input.GetAxis("Horizontal");
        float vertical_axis = Input.GetAxis("Vertical");

        float mouseX = Input.GetAxis("Mouse X");

        Vector3 direction =new Vector3 (horizontal_axis,0f, vertical_axis).normalized;
        if(direction.magnitude >=0.1f) {

            animator.SetBool("idle", false);
            animator.SetBool("walk", true);
            animator.SetBool("running", false);
            animator.SetBool("riflewalk", false);
            animator.SetBool("idleAim", false);

            transform.Rotate(Vector3.up * mouseX * playerSpeed * Time.deltaTime);
            float targetAngle = Mathf.Atan2(direction.x,direction.z)* Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turncalmTime);
            transform.rotation = Quaternion.Euler(0f,targetAngle,0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle,0f)* Vector3.forward;
            characterController.Move (transform.forward * playerSpeed * Time.deltaTime);

        }
        else
        {
            animator.SetBool("idle",true);
            animator.SetBool("walk",false);
            animator.SetBool("running", false);
        }
    }


    void Jump()
    {
        if (Input.GetButton("Jump")&& Onsurface)
        {
            animator.SetBool("idle", false );
            animator.SetTrigger("jump");
            velocity.y = Mathf.Sqrt(jumpRange * -2 * gravity);
        }
        else
        {
            animator.SetBool("idle", true);
            animator.ResetTrigger("jump");
        }
    }

    void Sprint()
    {
        if(Input.GetButton("Sprint")&& Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) && Onsurface)
        {
            float horizontal_axis = Input.GetAxis("Horizontal");
            float vertical_axis = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal_axis, 0f, vertical_axis).normalized;
            if (direction.magnitude >= 0.1f)
            {
                animator.SetBool("walk", false);
                animator.SetBool("running", true);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + playerCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turncalmTime);
                transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
                Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterController.Move(moveDirection.normalized * playerSprint * Time.deltaTime);

            }
            else
            {
                animator.SetBool("walk", true);
                animator.SetBool("running", false);
            }

        }
    }

    public void playerHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        
        playerHealthBar.SetHealth(presentHealth);
        if (presentHealth <= 0)
        {
            Playerdie();
        }
    }

    private void Playerdie()
    {
        endGameMenu.SetActive(true);
        Cursor.lockState =CursorLockMode.None;
        Object.Destroy(gameObject, 1.0f);
    }

    
}
