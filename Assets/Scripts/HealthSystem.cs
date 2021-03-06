using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void DieFunction();

public class HealthSystem : MonoBehaviour
{
    [SerializeField] float health = 100.0f;
    [SerializeField] GameManager gameManager;
    private float initialHealth = 0;
    private DieFunction die;
    
    public void setDieFunction(DieFunction die)
    {
        this.die = die;
    }
    public void OnEnable()
    {
        initialHealth = health;
    }
    public void takeDamage(float value)
    {
        health -= value;
        if (health <= 0.0f) gameManager.gameOver(); ;
    }

    internal void restart()
    {
        health = initialHealth;
    }
    public void kill()
    {
        health = 0;
        gameManager.gameOver();
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.M))
        {
            die();
        }*/
    }

    internal void takeDamage(object v)
    {
        throw new NotImplementedException();
    }
}
