using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GamePlayerController : MonoBehaviour
{
    [SerializeField] float camera_sensitivity, move_speed, sprint_speed, jumping_force, smooth_time;
    [SerializeField] GameObject CameraHolder;

    float CameraVerticalLook;
    bool bIsGrounded;
    Vector3 smoothMoveVelocity;
    Vector3 MovementAmount;

    Rigidbody rigidbody_Player;
    PhotonView PlayerController_PV;

    private void Awake()
    {
        rigidbody_Player = GetComponent<Rigidbody>();
        PlayerController_PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!PlayerController_PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rigidbody_Player);
        }
    }

    private void Update()
    {
        if (!PlayerController_PV.IsMine)
            return;
        CameraRotation();
        Movement();
        Jump();

    }

    public void CameraRotation()
    {
        transform.Rotate(transform.up * Input.GetAxisRaw("Mouse X") * camera_sensitivity);

        //**Horizontal camera not working as intended**//

        //CameraVerticalLook = Input.GetAxisRaw("Mouse Y") * camera_sensitivity;
        //CameraVerticalLook = Mathf.Clamp(CameraVerticalLook, -10f, 30f);

        //var camerarotaion = Quaternion.Euler(Vector3.right * CameraVerticalLook);
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, camerarotaion, smooth_time * Time.deltaTime);

    }

    public void Movement()
    {
        Vector3 MoveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;

        MovementAmount = Vector3.SmoothDamp(MovementAmount, MoveDirection * (Input.GetKey(KeyCode.LeftShift) ? sprint_speed : move_speed), ref smoothMoveVelocity, smooth_time);
    }

    public void Jump()
    {
        Debug.LogFormat("<color=red>Inside Jump Function</color>");
        if (Input.GetKey(KeyCode.Space) && bIsGrounded)
        {
            Debug.LogFormat("<color=green>Inside If Condition of Jump Function</color>");
            rigidbody_Player.AddForce(transform.up * jumping_force);
        }
    }

    public void SetIsGrounded (bool _grounded)
    {
        Debug.LogFormat("<color=yellow>Inside Grounded Function</color>");
        bIsGrounded = _grounded;
    }

    public void FixedUpdate()
    {
        if (!PlayerController_PV.IsMine)
            return;
        rigidbody_Player.MovePosition(rigidbody_Player.position + transform.TransformDirection(MovementAmount) * Time.fixedDeltaTime);
    }
}
