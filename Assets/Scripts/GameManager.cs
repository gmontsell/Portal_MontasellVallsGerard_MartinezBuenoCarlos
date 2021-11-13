using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private List<Transform> chekPoints;
   // [SerializeField] private Text textGameOver;

    private int last_chekPoint;

    private void Start()
    {
        setLastCheckpoint(0);
    }

    public void gameOver()
    {
        //textGameOver.gameObject.SetActive(true);
        Debug.Log("GG");
        //if (Input.GetKeyDown(KeyCode.F))
        //{
            Restart();
        //}
    }

    public void Restart()
    {
        //textGameOver.gameObject.SetActive(false);
        player.GetComponent<HealthSystem>().restart();
        player.GetComponent<CharacterController>().enabled = false;
        player.transform.position = chekPoints[last_chekPoint].position;
        player.transform.rotation = chekPoints[last_chekPoint].rotation;
        player.GetComponent<CharacterController>().enabled = true;

    }
    public void setLastCheckpoint(int chekPoint)
    {
        last_chekPoint = chekPoint;
    }
}
