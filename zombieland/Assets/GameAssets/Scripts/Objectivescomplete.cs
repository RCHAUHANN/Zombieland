using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectivescomplete : MonoBehaviour
{
    [Header("objectives to complete")]
    public Text no1;
    public Text no2;
    public Text no3;
    public Text no4;

    public static Objectivescomplete occurance;

    private void Awake()
    {
        occurance = this;
    }

    public void GeTObjectivesDone(bool ob1, bool obj2, bool obj3, bool obj4)
    {
        if(ob1 == true)
        {
            no1.text = "1. Complete";
            no1.color = Color.green;
        }
        else
        {
            no1.text = "01. Find the Rifle";
            no1.color = Color.white;
        }

        if (obj2 == true)
        {
            no2.text = "1. Complete";
            no2.color = Color.green;
        }
        else
        {
            no2.text = "02. locate all the hostages";
            no2.color = Color.white;
        }

        if (obj3 == true)
        {
            no3.text = "1. Complete";
            no3.color = Color.green;
        }
        {
            no3.text = "03. find the vehicle";
            no3.color = Color.white;
        }

        if (obj4 == true)
        {
            no4.text = "1. Complete";
            no4.color = Color.green;
        }
        {
            no4.text = "04. get all the hostages in the vehicle";
            no4.color = Color.white;
        }

    }
}
