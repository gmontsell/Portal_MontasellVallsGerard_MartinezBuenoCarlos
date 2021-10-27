using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField] private Portal otherPortal;
    [SerializeField] private Camera playerCam;
    [SerializeField] private Transform virtualPortal;
    [SerializeField] private Camera portalCamera;

    [SerializeField] private float nearClipOffset = 10f;


    private void Update()
    {
        Vector3 l_position = virtualPortal.InverseTransformPoint(playerCam.transform.position);
        otherPortal.portalCamera.transform.position = otherPortal.transform.TransformPoint(l_position);

        Vector3 local_direction = virtualPortal.InverseTransformPoint(playerCam.transform.forward);
        otherPortal.portalCamera.transform.forward = otherPortal.transform.TransformPoint(local_direction);
        otherPortal.portalCamera.GetComponent<Camera>().nearClipPlane = (playerCam.transform.position - transform.position).magnitude + nearClipOffset;
    }

}
