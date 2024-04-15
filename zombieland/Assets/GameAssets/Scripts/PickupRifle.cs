using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRifle : MonoBehaviour
{
    [Header("Rifle's")]
    public GameObject PlayerRifle;
    public GameObject pickuprifle;

    [Header("Rifle Assign Things")]
    public PlayerScript player;
    private float radius = 2.5f;

    private void Awake()
    {
        PlayerRifle.SetActive(false);
    }

    private void Update()
    {
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
