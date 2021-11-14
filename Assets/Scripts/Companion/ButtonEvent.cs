using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEvent : MonoBehaviour
{


    [SerializeField] UnityEvent buttonEvent;
    [SerializeField] string buttonName;
    [SerializeField] KeyCode buttonKeyCode;
    [SerializeField] Animation anim;
    [SerializeField] bool infinite;

    private bool todo=true;

   public bool pressButton()
    {
        bool canBeCalled = todo;
        if (infinite || todo)
        {
            
            buttonEvent.Invoke();
            todo = false;
        }
        return canBeCalled;
        
    }

    public void pressOpenDoor()
    {
        anim.Play("Door1");
        
    }

    internal void pressCloseDoor()
    {
        anim.Play("Door2");
       
    }

    public KeyCode getKeyCode()
    {
        return buttonKeyCode;
    }
    public string getButtonName()
    {
        return buttonName;
    }
}
