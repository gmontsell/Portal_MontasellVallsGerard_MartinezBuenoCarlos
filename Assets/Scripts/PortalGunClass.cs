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
    [SerializeField] private Vector3 initialSize = new Vector3(1.0f,1.85f,1.15f);
    private bool isActive;
    private float lastSize = 1;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            checkSize(previewPortal);
            previewPortal.SetActive(isActive);
            isActive = movePreviewPortal();  
        }
        if (Input.GetMouseButtonUp(0)&& isActive)
        {
            checkSize(bluePortal);
            bluePortal.SetActive(true);
            bluePortal.transform.SetPositionAndRotation(previewPortal.transform.position, previewPortal.transform.rotation);
            previewPortal.SetActive(false);
        }
        if (Input.GetMouseButtonUp(1)&& isActive)
        {
            checkSize(orangePortal);
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

    private void checkSize(GameObject  portal)
    {
        if (Input.GetAxis("Mouse ScrollWheel") >= 0f && lastSize<=2.0f )
        {
            lastSize += Input.GetAxis("Mouse ScrollWheel");
            portal.transform.localScale = initialSize * lastSize;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") <= 0f && lastSize >= 0.5f)
        {
            lastSize += Input.GetAxis("Mouse ScrollWheel");
            portal.transform.localScale = initialSize * lastSize;
        }
        
    }
}
