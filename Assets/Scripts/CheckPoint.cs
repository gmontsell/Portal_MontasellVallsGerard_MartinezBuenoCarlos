using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private int num_Chekpoint;
    [SerializeField] private Mesh greenMesh;
    [SerializeField] private Mesh redMesh;
    private void OnTriggerEnter(Collider other)
    {
        gameManager.setLastCheckpoint(num_Chekpoint);
    }
    public void activateChekPoint()
    {
        gameObject.GetComponent<MeshFilter>().mesh = greenMesh;
    }
    public void desactivateChekPoint()
    {
        gameObject.GetComponent<MeshFilter>().mesh = redMesh;
    }
}
