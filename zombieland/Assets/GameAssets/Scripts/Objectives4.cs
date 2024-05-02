using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Objectives4 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Objectivescomplete.occurance.GeTObjectivesDone(true, true, true, true);

            SceneManager.LoadScene("MainMenu"); 
        }
    }
}
