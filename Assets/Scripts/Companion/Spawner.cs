using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject objToSpawn;
    [SerializeField] private Vector3 MultiSpawnOffset;

    public void spawn(int amount)
    {
        for(int i=0;i<amount; i++)
           Instantiate(objToSpawn,transform.position + MultiSpawnOffset, transform.rotation);
    }
}
