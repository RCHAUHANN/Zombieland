using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    [Header("Player Punch")]
    public Camera cam;
    public float giveDamageof = 10f;
    public float punchingRange = 5f;

    [Header("Punch effects")]
    public GameObject woodeneffect;

    public void punch()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, punchingRange))
        {
            Debug.Log(hit.transform.name);
            ObjectToHit objectToHit = hit.transform.GetComponent<ObjectToHit>();



            if (objectToHit != null)
            {
                objectToHit.objectHitDamage(giveDamageof);
                GameObject impactGo = Instantiate(woodeneffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impactGo, 1f);
            }
        }
    }

}
