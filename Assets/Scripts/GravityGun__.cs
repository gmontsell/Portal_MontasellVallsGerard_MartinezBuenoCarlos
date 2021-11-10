using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun__ : MonoBehaviour
{
    Rigidbody takenObject;
    [SerializeField] Camera mainCamera;
    [SerializeField] float maxDistance;
    enum Status { taking, taken }
    Status currentStatus;

    [SerializeField] Transform attachPosition;
    Vector3 initialPosition;
    Quaternion initialRotation;
    [SerializeField]
    float moveSpeed;

    void Update()
    {
        if (Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("GravityShoot");
            takenObject = gravityShoot();
        }
        if (takenObject != null)
        {
            if (Input.GetMouseButton(2) && takenObject != null)
            {
                takenObject.isKinematic = true;
                switch (currentStatus)
                {
                    case Status.taking:
                        updateTaking();
                        break;
                    case Status.taken:
                        updateTaken();
                        break;
                }

            }
            else
            {
                detachObject(1000);
            }
        }
    }
    private Rigidbody gravityShoot()
    {
        Ray r = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
        if (Physics.Raycast(r, out RaycastHit hitInfo, maxDistance)) ;
        {
            if (hitInfo.rigidbody != null)
            {

                hitInfo.rigidbody.isKinematic = true;
                if (hitInfo.rigidbody.gameObject.TryGetComponent<Teleportable>(out Teleportable tp)) tp.isActive = false;
                initialPosition = hitInfo.transform.position;
                initialRotation = hitInfo.transform.rotation;
                currentStatus = Status.taking;
                return hitInfo.rigidbody;
            }
        }

        return null;
    }
    private void updateTaking()
    {
        takenObject.MovePosition(takenObject.position + (attachPosition.position - takenObject.position).normalized * moveSpeed * Time.deltaTime);

        takenObject.rotation = Quaternion.Lerp(initialRotation, attachPosition.rotation, (takenObject.position - initialPosition).magnitude / (attachPosition.position - initialPosition).magnitude);

        if ((attachPosition.position - takenObject.position).magnitude < 0.1f) currentStatus = Status.taken;
    }
    private void updateTaken()
    {
        takenObject.transform.position = attachPosition.position;
        takenObject.transform.rotation = attachPosition.rotation;
    }

    private void detachObject(float force)
    {
        if (takenObject.gameObject.TryGetComponent<Teleportable>(out Teleportable tp)) tp.isActive = true;
        takenObject.isKinematic = false;
        takenObject.AddForce(attachPosition.forward * force);
        takenObject = null;
    }
}

