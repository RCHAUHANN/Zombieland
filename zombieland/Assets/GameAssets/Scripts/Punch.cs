using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    [Header("Player Punch")]
    public Camera cam;
    public float giveDamageof = 10f;
    public float punchingRange = 5f;

    

    public void punch()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, punchingRange))
        {
            Debug.Log(hit.transform.name);
            ObjectToHit objectToHit = hit.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hit.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hit.transform.GetComponent<Zombie2>();


            if (objectToHit != null)
            {
                objectToHit.objectHitDamage(giveDamageof);
                
            }
            else if (zombie1 != null)
            {
                zombie1.zombieHitDamage(giveDamageof);
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamageof);
            }
        }
    }

}
