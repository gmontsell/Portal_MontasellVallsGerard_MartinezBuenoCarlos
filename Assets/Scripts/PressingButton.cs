using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressingButton : MonoBehaviour
{
    private List<ButtonEvent> activeButtons = new List<ButtonEvent>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ButtonEvent button))
        {
            //activeButtons.Add(button);
            button.pressOpenDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ButtonEvent button))
        {
            //activeButtons.Remove(button);
            button.pressCloseDoor();
        }
    }
    private void Update()
    {
        foreach (ButtonEvent button in activeButtons)
        {
            if (Input.GetKeyDown(button.getKeyCode())) button.pressButton();

        }
    }
}
