using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewPortal : MonoBehaviour
{

    [SerializeField] private List<Transform> controlPoints;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private string escenariTag;
    [SerializeField] private float maxNormalAngle;
    [SerializeField] private float maxDistance;

    public bool isValidPosition(Camera mainCam)
    {
        foreach(var point in controlPoints)
        {
           if( Physics.Raycast(mainCam.transform.position, point.position - mainCam.transform.position,
                out RaycastHit hitInfo, float.MaxValue, layerMask))
            {
                if (hitInfo.transform.gameObject.CompareTag(escenariTag))
                { 
                    Debug.Log("Invalid position: TAG");
                    return false;
                   
                }
                if (Vector3.Angle(hitInfo.normal, point.forward) > maxNormalAngle)
                {
                    Debug.Log("Invalid position: Angle");
                    return false;
                    
                }
                if ((hitInfo.point - point.position).magnitude > maxDistance)
                {
                    Debug.Log("Invalid position: Distance");
                    return false;
                }
                return true;
            }
            else
            {
                return false;
            }
           
            //if !portal_enabled --> return false
            // if angle (normal, lastNormal) > angleTheshold --> return false;
            //if dist(controlPoint, raycastPoint) > distThreeshold -->return false;
        }
        return false;
    }
}
