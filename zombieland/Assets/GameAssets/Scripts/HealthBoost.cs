using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : MonoBehaviour
{
    [Header("HealthBoost")]
    public PlayerScript PlayerScript;
    private float healthToGive = 120f;
    private float radius = 2.5f;

    [Header("Healthboost Animator")]
    public Animator animator;

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerScript.transform.position) < radius)
        {
            if (Input.GetKeyDown("f"))
            {
                animator.SetBool("Open", true);
                PlayerScript.presentHealth = healthToGive;

                Object.Destroy(gameObject, 1.5f);
            }
        }
    }

}
