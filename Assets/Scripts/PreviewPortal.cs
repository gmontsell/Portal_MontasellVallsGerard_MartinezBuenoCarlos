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
                if ((hitInfo.point - point.position).magnitude > maxDistance)
                {
                    return false;
                }
               
                if (Vector3.Angle(hitInfo.normal, point.forward) > maxNormalAngle)
                {
                    return false;
                    
                }

                if (!hitInfo.transform.gameObject.CompareTag(escenariTag))
                {
                    return false;
                }
           }
            else
            {
                return false;
            }
        }
        return true;
    }
}
