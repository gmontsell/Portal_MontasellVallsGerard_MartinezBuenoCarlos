using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Teleportable : MonoBehaviour
{
    [SerializeField] private float teleportOffset;
    private bool isTeleporting = false;
    private Vector3 teleportPosition;
    private Vector3 teleportForward;
    [SerializeField]public bool isActive;

    private void OnTriggerEnter(Collider other)
    {

      
        if (other.gameObject.TryGetComponent(out Portal portal)&&isActive)
        {
            Debug.Log("Teleport");
            isTeleporting = true;
            Vector3 l_Position = portal.virtualPortal.transform.InverseTransformPoint(transform.position);
            Vector3 l_Direction = portal.virtualPortal.transform.InverseTransformDirection(transform.forward);

            teleportForward = portal.otherPortal.transform.TransformDirection(l_Direction);
            teleportPosition = portal.otherPortal.transform.TransformPoint(l_Position) + portal.otherPortal.transform.forward * teleportOffset;

            if(TryGetComponent(out Rigidbody rb))
            {
                Vector3 l_Velocity = portal.virtualPortal.transform.InverseTransformDirection(rb.velocity);
                rb.velocity = portal.otherPortal.transform.TransformDirection(l_Velocity);
                transform.localScale *=  (portal.otherPortal.transform.localScale.x / portal.transform.localScale.x);
            }

        }
    }
    private void LateUpdate()
    {
        if (isTeleporting)
        {
            isTeleporting = false;
            if(TryGetComponent(out CharacterController charController))
            {
                charController.enabled = false;
            }
            transform.position = teleportPosition;
            transform.forward = teleportForward;

            if(TryGetComponent(out FPSController_0 fpsController))
            {
                fpsController.recalculatePictchAndYaw();
            }
            if (TryGetComponent(out CharacterController characterController))
            {
                characterController.enabled = true;
            }
        }

    }
}
