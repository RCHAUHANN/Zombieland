using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float playerSpeed = 1.9f;

    [Header("Player Script Camera")]
    public Transform playerCamera;

    [Header("Player Animator and Gravity")]
    public CharacterController characterController;
    public float gravity = -9.81f;

    [Header("Player Jumping and velocity")]
    public float turncalmTime = 0.1f;
    float turnCalmVelocity;
    public float jumpRange = 1f;
    Vector3 velocity;
    public Transform surfaceCheck;
    bool Onsurface;
    public float surfaceDistance;
    public LayerMask surfaceMask;



    private void Update()
    {
        Onsurface =Physics.CheckSphere(surfaceCheck.position, surfaceDistance,surfaceMask);
        if (Onsurface && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity *Time.deltaTime);
        playerMove();
    }

    void playerMove()
    {
        float horizontal_axis = Input.GetAxis("Horizontal");
        float vertical_axis = Input.GetAxis("Vertical");

        Vector3 direction =new Vector3 (horizontal_axis,0f, vertical_axis).normalized;
        if(direction.magnitude >=0.1f) {

            float targetAngle = Mathf.Atan2(direction.x,direction.z)* Mathf.Rad2Deg + playerCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnCalmVelocity, turncalmTime);
            transform.rotation = Quaternion.Euler(0f,targetAngle,0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle,0f)* Vector3.forward;
            characterController.Move (direction.normalized * playerSpeed * Time.deltaTime);

        }
    }
}
