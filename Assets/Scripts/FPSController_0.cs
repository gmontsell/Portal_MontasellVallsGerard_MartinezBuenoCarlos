using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController_0 : MonoBehaviour
{
    float mYaw;
    float mPitch;
    bool mOnGround;
    bool m_Contact_Celing;
    float mVerticalSpeed;



    [SerializeField] float M_Speed_Yaw;
    [SerializeField] float M_Speed_Pitch;
    [SerializeField] float M_Min_Pitch;
    [SerializeField] float M_Max_Pitch;
    [SerializeField] GameObject M_Pitch_Controller;
    [SerializeField] bool M_Invert_Pitch;
    [SerializeField] bool M_Invert_Yaw;

    [Header("Move")]
    [SerializeField] CharacterController M_Character_Controller;
    [SerializeField] float M_Speed;
    public KeyCode M_Forward = KeyCode.W;
    public KeyCode M_Back = KeyCode.S;
    public KeyCode M_Left = KeyCode.A;
    public KeyCode M_Right = KeyCode.D;
    public KeyCode M_Jump = KeyCode.Space;
    public KeyCode M_Run = KeyCode.LeftShift;
    public float M_Jump_High;
    public float M_Jump_Time;
    public float M_Speed_Multiplayer;

    [Header("Block")]
    public KeyCode m_DebugLockAngleKeyCode = KeyCode.I;
    public KeyCode m_DebugLockKeyCode = KeyCode.O;
    bool m_AngleLocked = false;
    bool m_AimLocked = false;

    private void OnEnable()
    {
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }

    private void OnDestroy()
    {
        
    }
    void Awake()
    {
        mYaw = transform.rotation.eulerAngles.y;
        mPitch = M_Pitch_Controller.transform.eulerAngles.x;
    }
    void Update()
    {
        if (!m_AngleLocked) Rotate();
        Move();

        if (Input.GetKeyDown(m_DebugLockAngleKeyCode))
            m_AngleLocked = !m_AngleLocked;
        if (Input.GetKeyDown(m_DebugLockKeyCode))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
                Cursor.lockState = CursorLockMode.None;
            else
                Cursor.lockState = CursorLockMode.Locked;
            m_AimLocked = Cursor.lockState == CursorLockMode.Locked;
        }
    }

    private void Move()
    {
        Vector3 forward = new Vector3(
            Mathf.Sin(mYaw * Mathf.Deg2Rad),
            0.0f,
            Mathf.Cos(mYaw * Mathf.Deg2Rad));
        Vector3 right = new Vector3(
            Mathf.Sin((mYaw + 90.0f) * Mathf.Deg2Rad),
            0.0f,
            Mathf.Cos((mYaw + 90.0f) * Mathf.Deg2Rad)
            );
        Vector3 lMovement = new Vector3();
        if (Input.GetKey(M_Forward)) lMovement += forward;
        else if (Input.GetKey(M_Back)) lMovement -= forward;
        if (Input.GetKey(M_Right)) lMovement += right;
        else if (Input.GetKey(M_Left)) lMovement -= right;

        if (mOnGround && Input.GetKey(M_Jump)) mVerticalSpeed = (2 * M_Jump_High / M_Jump_Time);

        lMovement.Normalize();

        lMovement *= M_Speed * Time.deltaTime * (Input.GetKey(M_Run) ? M_Speed_Multiplayer : 1.0f);

        mVerticalSpeed += (-2 * M_Jump_High / M_Jump_Time / M_Jump_Time) * Time.deltaTime;
        lMovement.y = mVerticalSpeed * Time.deltaTime;

        CollisionFlags colls = M_Character_Controller.Move(lMovement);
        mOnGround = (colls & CollisionFlags.Below) != 0;
        m_Contact_Celing = (colls & CollisionFlags.Above) != 0;

        if (mOnGround) mVerticalSpeed = 0.0f;
        if (m_Contact_Celing && mVerticalSpeed > 0.0f) mVerticalSpeed = 0.0f;
    }

    private void Rotate()
    {
        float xMousePos = Input.GetAxis("Mouse X");
        float yMousePos = Input.GetAxis("Mouse Y");
        mYaw += xMousePos * M_Speed_Yaw * (M_Invert_Yaw ? -1 : 1);
        mPitch -= yMousePos * M_Speed_Pitch * (M_Invert_Pitch ? -1 : 1);
        mPitch = Mathf.Clamp(mPitch, M_Min_Pitch, M_Max_Pitch);
        transform.rotation = Quaternion.Euler(0.0f, mYaw, 0.0f);
        M_Pitch_Controller.transform.localRotation = Quaternion.Euler(mPitch, 0, 0);

    }

    public void modifMYaw(float x)
    {
        mYaw += x;
    }

    public void modifMPitch(float y)
    {
        mPitch += y;
    }
}
