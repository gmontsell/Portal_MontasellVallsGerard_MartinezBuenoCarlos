using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private int num_Chekpoint;

    private void OnTriggerEnter(Collider other)
    {
        gameManager.setLastCheckpoint(num_Chekpoint);
    }

    private int getNumCheckPoint()
    {
        return num_Chekpoint;

    }
}
