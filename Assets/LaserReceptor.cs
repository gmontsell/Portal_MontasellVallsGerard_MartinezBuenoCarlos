using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserReceptor : MonoBehaviour
{
    [SerializeField] Animation anim;
    private bool open = false;
    public void openDoor()
    {
        if (!open)
        {
            anim.Play("Door1");
            open = true;
        }

    }

    public void closeDoor()
    {
        if (open)
        {
            anim.Play("Door2");
            open = false;
        }
        
    }
}
