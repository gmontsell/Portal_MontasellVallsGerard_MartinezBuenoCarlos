using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGunClass : MonoBehaviour
{

    [SerializeField] private string portalEnableTag;
    [SerializeField] private GameObject previewPortal;
    [SerializeField] private Camera mainCam;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask portalLayerMask;
    [SerializeField] private GameObject orangePortal;
    [SerializeField] private GameObject bluePortal;
    private bool isActive;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            isActive = movePreviewPortal();
        }
        previewPortal.SetActive(isActive);
        if (Input.GetMouseButtonUp(0)&& isActive)
        {
            bluePortal.SetActive(true);
            bluePortal.transform.SetPositionAndRotation(previewPortal.transform.position, previewPortal.transform.rotation);
            previewPortal.SetActive(false);
        }
        if (Input.GetMouseButtonUp(1)&& isActive)
        {
            orangePortal.SetActive(true);
            orangePortal.transform.SetPositionAndRotation(previewPortal.transform.position, previewPortal.transform.rotation);
            previewPortal.SetActive(false);
        }
        
    }

    private bool movePreviewPortal()
    {
        Ray r = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        if(Physics.Raycast(r, out RaycastHit hitInfo, maxDistance, portalLayerMask))
        {
            if (hitInfo.transform.gameObject.CompareTag(portalEnableTag))
            {
                Debug.Log("FUncioma");
                previewPortal.transform.position = hitInfo.point;
                previewPortal.transform.forward = hitInfo.normal;
                return previewPortal.GetComponent<PreviewPortal>().isValidPosition(mainCam);
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
