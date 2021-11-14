using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject player;
    [SerializeField] private List<Transform> chekPoints;
    [SerializeField] private TextMeshProUGUI textGameOver;

    private int last_chekPoint;

    private void Start()
    {
        setLastCheckpoint(0);
    }

    public void gameOver()
    {
        player.GetComponent<CharacterController>().enabled = false;
        textGameOver.gameObject.SetActive(true);
        Debug.Log("GG");
       
        if (Input.GetKeyDown(KeyCode.F))
        {
            Restart();
        }
    }

    public void Restart()
    {
        textGameOver.gameObject.SetActive(false);
        //textGameOver.gameObject.SetActive(false);

        if (last_chekPoint == 0)
        {
            SceneManager.LoadScene("LevelDesign");
        }
        else
        {
            player.GetComponent<HealthSystem>().restart();
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = chekPoints[last_chekPoint].position;
            player.transform.rotation = chekPoints[last_chekPoint].rotation;
            player.GetComponent<CharacterController>().enabled = true;
        }
      

    }
    public void setLastCheckpoint(int chekPoint)
    {
        last_chekPoint = chekPoint;
    }
}
