using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombiespawn : MonoBehaviour
{
    [Header("ZombieSpawn Var")]
    public GameObject zombiePrefab;
    public Transform zombieSpawnPosition;
    public GameObject DangerZone1;
    private float repeatCycle = 1f;



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            StartCoroutine(DangerZoneTime());
            Destroy(gameObject, 10f);

            gameObject.GetComponent<BoxCollider>().enabled = false;

        }
    }

    void EnemySpawner()
    {
        Instantiate(zombiePrefab,zombieSpawnPosition.position, zombieSpawnPosition.rotation);
    }

    IEnumerator DangerZoneTime()
    {
        DangerZone1.SetActive(true);
        yield return new WaitForSeconds(5f);
        DangerZone1.SetActive(false);
    }

}
