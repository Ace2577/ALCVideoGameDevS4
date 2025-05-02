using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerFPS : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed; //movment speed in units per second
    public float jumpForce; //force up
    [Header("Camera")]
    public float lookSensitivity; //camera sensitivity
    public float maxLookX; //lowest we can look down
    public float minLookX; //highest we can look
    private float rotx; //current x rotation on camera
    private Camera cam; //gets the camera
    private Rigidbody rig; //gets the rig
    //private ProjectileWeapon weapon;

    void Awake() //happens before start
    {
        cam=Camera.main; //gets the camera
        rig=GetComponent<Rigidbody>(); //gets the rig
        Cursor.lockState=CursorLockMode.Locked; //disables and hides the cursor
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        CamLook();

        if(Input.GetButtonDown("Jump"))
            TryJump();

        /* if (InputGetButton("Fire1"))
        {
            if(weapon.CanShoot())
        }
        */
    }
    void Move()
    {
        float x=Input.GetAxis("Horizontal")*moveSpeed;
        float z=Input.GetAxis("Vertical")*moveSpeed;
        Vector3 dir=transform.right*x +transform.forward*z;
        dir.y = rig.velocity.y;
        rig.velocity=dir;
    }
    void CamLook()
    {
       
        float y = Input.GetAxis("Mouse X")*lookSensitivity;
        rotx += Input.GetAxis("Mouse Y")*lookSensitivity;
        rotx = Mathf.Clamp(rotx,minLookX,maxLookX);
        cam.transform.localRotation=Quaternion.Euler(-rotx,0,0); //moves us to camera
        transform.eulerAngles += Vector3.up * y;
     
    }
    void TryJump()
    {
     
        Ray ray=new Ray(transform.position,Vector3.down);
        if(Physics.Raycast(ray,1.1f))
            rig.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
     
    }
}
