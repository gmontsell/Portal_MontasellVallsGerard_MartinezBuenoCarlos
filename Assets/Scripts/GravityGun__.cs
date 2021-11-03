﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGun__ : MonoBehaviour
{

    Rigidbody takenObject;
    enum Status { taking, taken }
    Status currentStatus;
    [SerializeField] Camera cam;
    [SerializeField] Transform attachPosition;
    Vector3 initialPosition;
    Quaternion initialRotation;
    [SerializeField]
    float moveSpeed;

    void Update()
    {
        if(Input.GetMouseButtonDown(2))
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

                if(Input.GetKeyDown(KeyCode.T)) detachObject(1000);

            }
            else
            {
                detachObject(1000);
            }
        }
    }

    private void detachObject()
    {
        throw new NotImplementedException();
    }

    private void detachObject(float force)
    {
        takenObject.isKinematic = false;
        takenObject.AddForce(attachPosition.forward * force);
        takenObject = null;
    }

    private void updateTaken()
    {
        takenObject.transform.position = attachPosition.position;
        takenObject.transform.rotation = attachPosition.rotation;
    }

    private void updateTaking()
    {
        takenObject.MovePosition(
            takenObject.position + (attachPosition.position - takenObject.position).normalized
            * moveSpeed * Time.deltaTime);

        takenObject.rotation = Quaternion.Lerp(initialRotation, attachPosition.rotation,
            (takenObject.position - initialPosition).magnitude / (attachPosition.position - initialPosition).magnitude);


        if ((attachPosition.position - takenObject.position).magnitude < 0.1f) currentStatus = Status.taken;

    }

    private Rigidbody gravityShoot()
    {
        if (Physics.Raycast(cam.ViewportPointToRay(new Vector3(0.5f, 0.5f)), out RaycastHit hit, 200.0f))
        {
            Rigidbody rb = hit.rigidbody;
            if (rb == null) return null;
            rb.isKinematic = true;
            initialPosition = rb.transform.position;
            initialRotation = rb.transform.rotation;
            currentStatus = Status.taking;
            return rb;

        }
        return null;
    }
}
