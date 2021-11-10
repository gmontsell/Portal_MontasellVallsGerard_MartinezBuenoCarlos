using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_Zone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("DEAD");
        //HealthSystem hs = other.GetComponent<HealthSystem>();
        //if (hs != null) hs.takeDamage(999);
    }   
}

