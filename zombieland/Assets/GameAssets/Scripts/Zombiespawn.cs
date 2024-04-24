using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombiespawn : MonoBehaviour
{
    [Header("ZombieSpawn Var")]
    public GameObject zombiePrefab;
    public Transform zombieSpawnPosition;

    private float repeatCycle = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    void EnemySpawner()
    {
        Instantiate(zombiePrefab,zombieSpawnPosition.position, zombieSpawnPosition.rotation);
    }

}
