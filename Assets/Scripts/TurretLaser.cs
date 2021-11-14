using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLaser : MonoBehaviour
{

    [SerializeField] LineRenderer laserRenderer;

    [SerializeField] LayerMask laserLayerMask;
    [SerializeField] private float maxLaserDist;
    [SerializeField] private bool isActive;
    [SerializeField] private bool isTurret;
    private GameObject lastLaserObj;
    private GameObject lastReceptor;
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
                laserRenderer.SetPosition(1, Vector3.forward * hitInfo.distance);

                if (hitInfo.transform.gameObject.TryGetComponent<TurretLaser>(out TurretLaser laser))
                {
                    lastLaserObj = laser.gameObject;
                    laser.updateState(true);
                }
                else if(hitInfo.transform.gameObject.TryGetComponent<LaserReceptor>(out LaserReceptor receptor) && !isTurret)
                {
                    lastReceptor = receptor.gameObject;
                    receptor.openDoor();
                }
                else if (lastLaserObj != null)
                {
                    lastLaserObj.GetComponent<TurretLaser>().updateState(false);
                    lastLaserObj = null;
                }
                else if (lastReceptor != null && !isTurret)
                {
                    lastReceptor.GetComponent<LaserReceptor>().closeDoor();
                    lastReceptor = null;
                }
                if (hitInfo.transform.gameObject.TryGetComponent<HealthSystem>(out HealthSystem hs) && isTurret) hs.kill();
            }
            else
            {
                laserRenderer.SetPosition(1, laserRenderer.transform.forward * maxLaserDist);
            }
        }
        else{
            updateState(false);
        }
    }
}
