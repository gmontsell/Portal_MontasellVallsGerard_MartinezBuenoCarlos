using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonEvent : MonoBehaviour
{

    [SerializeField] private KeyCode buttonKeyCode;
    [SerializeField] private string buttonName;
    [SerializeField] private UnityEvent buttonEvent;
    [SerializeField] private bool onceTrigger;
    private bool isActive = true;
    public bool pressButton()
    {
        bool canBeCalled = isActive;
        if (canBeCalled)
        {
            buttonEvent.Invoke();
        }
        if (onceTrigger) isActive = false;
        return canBeCalled;
    }

    public KeyCode GetKeyCode()
    {
        return buttonKeyCode;
    }
    public string getButtonName()
    {
        return buttonName;
    }

}
