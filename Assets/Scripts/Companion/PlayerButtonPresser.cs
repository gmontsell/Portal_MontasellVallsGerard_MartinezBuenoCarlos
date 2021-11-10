using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonPresser : MonoBehaviour
{
    //[SerializeField] private KeyCode key =  KeyCode.M;
    private List<ButtonEvent> activeButtons = new List<ButtonEvent>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out ButtonEvent button))
        {
            activeButtons.Add(button);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out ButtonEvent button))
        {
            activeButtons.Remove(button);
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
