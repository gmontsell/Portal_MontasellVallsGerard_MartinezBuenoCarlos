using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
   [SerializeField] private PortalCamera otherPortal;
   [SerializeField] private Transform playerCamTransform;
   [SerializeField] private Transform virtualPortal;
    [SerializeField] private Transform portalCamera;

    [SerializeField] private float nearClipOffset=10f;


    private void Update()
    {
        Vector3 l_position = virtualPortal.InverseTransformPoint(playerCamTransform.position);
        otherPortal.portalCamera.transform.position = otherPortal.transform.TransformPoint(l_position);

        float playerDist = (playerCamTransform.position - transform.position).magnitude;
        otherPortal.portalCamera.GetComponent<Camera>().nearClipPlane = playerDist + nearClipOffset;
        otherPortal.portalCamera.GetComponent<Camera>().fieldOfView=100/(playerDist);
    }


}
