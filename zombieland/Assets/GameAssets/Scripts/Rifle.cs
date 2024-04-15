using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle things")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float shootingRange = 100f;
    public float FireCharge = 15f;
    private float NextTimeToShoot = 0f;
    public PlayerScript player;
    public Transform hand;
    public Animator animator;

    [Header("Rifle ammunition and shooting")]
    private int maxAmmunition = 32;
    public int mag = 10;
    private int Presentammunition;
    public float reloadingTime = 1.3f;
    private bool setReloading = false;

    [Header("Rifle effects")]
    public ParticleSystem muzzlespark;
    public GameObject woodenEffect;



    private void Awake()
    {
        transform.SetParent(hand);
        Presentammunition =maxAmmunition;
    }

    void Update()
    {
        if (setReloading)
        {
            return;
        }
        if(Presentammunition <=0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= NextTimeToShoot)
        {
            animator.SetBool("fire",true);
            animator.SetBool("idle", false);
            NextTimeToShoot = Time.time + 1f / FireCharge;
            shoot();
        }
        else if(Input.GetButton("Fire1") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("idle",false);
            animator.SetBool("firewalk", true);

        }
        else if (Input.GetButton("Fire1") && Input.GetButton("Fire2"))
        {
            animator.SetBool("idle", false);
            animator.SetBool("idleAim", true);
            animator.SetBool("firewalk",true );
            animator.SetBool("walk", true );
            animator.SetBool("reloading",false );

        }
        else
        {
            animator.SetBool("fire", false);
            animator.SetBool("idle",true);
            animator.SetBool("firewalk", false);

        }
        
    }
    void shoot()
    {
        if(mag == 0)
        {
            return;
        }
        Presentammunition--;
        if(Presentammunition == 0)
        {
            mag--;
        }
        

        muzzlespark.Play();
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, shootingRange)) 
        {
            Debug.Log(hit.transform.name);
            ObjectToHit objectToHit = hit.transform.GetComponent<ObjectToHit>();

            

            if (objectToHit != null)
            {
                objectToHit.objectHitDamage(giveDamageOf);
                GameObject impactGo = Instantiate(woodenEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGo, 1f);
            }
        }
    }

    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSpeed = 0f;
        setReloading =true;
        Debug.Log("reloading....");
        animator.SetBool("reloading",true);

        yield return new WaitForSeconds(reloadingTime);
        animator.SetBool("reloading", false);   
        Presentammunition = maxAmmunition;
        player.playerSpeed = 1.9f;
        player.playerSprint = 3f;
        setReloading = false;

    }

 
   
}
