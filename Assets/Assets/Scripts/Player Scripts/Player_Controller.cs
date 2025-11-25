using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player_Controller : MonoBehaviour
{
    [Header("References")] 
    public Rigidbody rb;
    public Transform head;
    public Camera playerCamera;
    
    [Header("Player Speed")]
    public float walkSpeed;
    public float runSpeed;

    [Header("Jump Settings")]
    public float jumpSpeed;
    
    private Vector3 _newVelocity;
    private bool _isGrounded = false;
    private bool _isJumping = false;
        
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if(LockingInteraction.ins.lockingObject != null)
            return;
        CameraMovement();
        PlayerMovement();
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        CameraRotation();
    }
    
    public void PlayerMovement()
    {
        _newVelocity = Vector3.up * rb.linearVelocity.y;
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;
        _newVelocity.x = Input.GetAxis("Horizontal") * speed;
        _newVelocity.z = Input.GetAxis("Vertical") * speed;
        
        if (_isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_isJumping)
            {
                //Debug.Log("Jumping ");
                _newVelocity.y = jumpSpeed;
                _isJumping = true;
            }
        }
        
        rb.linearVelocity = transform.TransformDirection(_newVelocity);
    }

    public static float RestrictAngle(float angle, float minAngle, float maxAngle)
    {
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;
        if (angle > maxAngle)
            angle = maxAngle;
        if (angle < minAngle)
            angle = minAngle;
        return angle;
    }
    public void CameraMovement()
    {
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * 2f);
    }

    public void CameraRotation()
    {
        Vector3 e = head.eulerAngles;
        e.x -= Input.GetAxis("Mouse Y") * 2f;
        e.x = RestrictAngle(e.x, -85f, 85f);
        head.eulerAngles = e;
    }

    void OnCollisionStay(Collision col)
    {
        _isGrounded = true;
        _isJumping = false;
    }

    void OnCollisionExit(Collision col)
    {
        _isGrounded = false;
    }
}
