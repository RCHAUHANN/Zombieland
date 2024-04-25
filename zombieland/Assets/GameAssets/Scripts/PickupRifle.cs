using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRifle : MonoBehaviour
{
    [Header("Rifle's")]
    public GameObject PlayerRifle;
    public GameObject pickuprifle;
    public Punch playerPunch;
    public GameObject RifleUI;


    [Header("Rifle Assign Things")]
    public PlayerScript player;
    private float radius = 2.5f;
    private float nextTimeToPunch = 0f;
    public float punchCharge = 15f;
    public Animator animator;

    private void Awake()
    {
        PlayerRifle.SetActive(false);
        RifleUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToPunch ) 
        {
            animator.SetBool("punch",true);
            animator.SetBool("idle",false);
            nextTimeToPunch = Time.time + 1f/punchCharge;
            playerPunch.punch();

        }
        else
        {
            animator.SetBool("punch",false) ;
            animator.SetBool("idle", true);
        }
        if(Vector3.Distance(transform.position,player.transform.position) < radius)
        {
            if (Input.GetKeyDown("f"))
            {
                PlayerRifle.SetActive(true);
                pickuprifle.SetActive(false);
            }
        }
    }


}
