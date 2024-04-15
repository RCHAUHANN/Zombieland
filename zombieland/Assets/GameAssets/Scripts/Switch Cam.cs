using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCam : MonoBehaviour
{
    [Header("Camera to assign")]
    public GameObject AimCam;
    public GameObject AimCanvas;
    public GameObject ThirdPersonCam;
    public GameObject ThirdPersonCanvas;

    [Header("Camera animator")]
    public Animator animator;


    private void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            animator.SetBool("idle", false);
            animator.SetBool("idleAim", true);
            animator.SetBool("rifleWalk", true);
            animator.SetBool("walk", true);


            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);

        }
        else if (Input.GetButton("Fire2"))
        {
            animator.SetBool("idle", false);
            animator.SetBool("idleAim", true);
            animator.SetBool("rifleWalk", false);
            animator.SetBool("walk", false);


            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            AimCam.SetActive(true);
            AimCanvas.SetActive(true);

        }
        else
        {
            animator.SetBool("idle", true);
            animator.SetBool("idleAim", false);
            animator.SetBool("rifleWalk", false);
            

            ThirdPersonCam.SetActive(true );
            ThirdPersonCanvas.SetActive(true );
            AimCam.SetActive(false);
            AimCanvas.SetActive(false);
        }
    }

}
