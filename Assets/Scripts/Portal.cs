using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
   [SerializeField] public Portal otherPortal;
   [SerializeField] private Transform playerCamTransform;
   [SerializeField] public Transform virtualPortal;
    [SerializeField] private Transform portalCamera;

    [SerializeField] private float nearClipOffset=10f;


    private void Update()
    {
        Vector3 l_position = virtualPortal.InverseTransformPoint(playerCamTransform.position);
        otherPortal.portalCamera.transform.position = otherPortal.transform.TransformPoint(l_position);

       /* float playerDist = (playerCamTransform.position - transform.position).magnitude;
        otherPortal.portalCamera.GetComponent<Camera>().nearClipPlane = playerDist + nearClipOffset;
        //otherPortal.portalCamera.GetComponent<Camera>().fieldOfView=100/(playerDist);*/

        Vector3 local_direction = virtualPortal.InverseTransformDirection(playerCamTransform.transform.forward);
        otherPortal.portalCamera.transform.forward = otherPortal.transform.TransformDirection(local_direction);
        otherPortal.portalCamera.GetComponent<Camera>().nearClipPlane = (playerCamTransform.position - transform.position).magnitude + nearClipOffset;
    }


}
