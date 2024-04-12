using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    [Header("Rifle things")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float shootingRange = 100f;

    void shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, shootingRange)) 
        {
            Debug.Log(hit.transform.name);
        }
    }

 
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        } 
    }
}
