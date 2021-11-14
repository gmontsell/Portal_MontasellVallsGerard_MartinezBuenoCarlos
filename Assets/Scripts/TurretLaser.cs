using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{

    [SerializeField] LineRenderer laserRenderer;

    [SerializeField] LayerMask laserLayerMask;
    [SerializeField] private float maxLaserDist=2000;
    [SerializeField] private bool isActive;
    [SerializeField] private bool isTurret;

    private void Start()
    {
        Debug.Log("Start dist " + maxLaserDist);
    }
    public void  updateState(bool isActive)
    {

        laserRenderer.enabled = isActive;
        this.isActive = isActive;
    }

    private void Update()
    {
        if (isActive)
        {
            Ray r = new Ray(laserRenderer.transform.position, laserRenderer.transform.forward);
            if (Physics.Raycast(r, out RaycastHit hitInfo, maxLaserDist, laserLayerMask))
            {
                Debug.Log("Principio dist " + maxLaserDist);
                Debug.Log("Principio"+laserRenderer.GetPosition(1));
                laserRenderer.SetPosition(1, Vector3.forward * hitInfo.distance);

                if(hitInfo.transform.gameObject.TryGetComponent<TurretLaser>(out TurretLaser laser))
                {
                    Debug.Log("If "+laserRenderer.GetPosition(1));
                    laser.updateState(true);
                }
                if (hitInfo.transform.gameObject.TryGetComponent<HealthSystem>(out HealthSystem hs) && isTurret) hs.kill();
            }else
            {
                Debug.Log("else dist " + maxLaserDist);
                Debug.Log("else laser forward " + laserRenderer.transform.forward);
                laserRenderer.SetPosition(1, laserRenderer.transform.forward * maxLaserDist);
                Debug.Log("Else " + laserRenderer.GetPosition(1));
            }
        }
    }
}
