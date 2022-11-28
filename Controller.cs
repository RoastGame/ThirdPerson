using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Controller : MonoBehaviour
{
    [Header("Veriable")]
    [Range(1, 20)]
    public float rotationspeed;
    [Range(1, 20)]
    public float StrafeTurnSpeed;
    public float delay;
    float normalFov;
    public float SprintFov;

    float inputX;
    float inputY;
    float MaxSpeed;

    public KeyCode SprintButton = KeyCode.LeftShift;
    public KeyCode WalkButton = KeyCode.LeftControl;

    public Transform Model;

    Animator Anim;
    Vector3 StickDirection;
    Camera mainCam;

    public enum MovementType
    {
        Directional,
        Strafe
    };

    public MovementType HareketTipi;




   

    



    void Start()
    {
        Anim = GetComponent<Animator>();
        mainCam = Camera.main;
        normalFov = mainCam.fieldOfView;
    }


    private void LateUpdate()
    {
        InputMove();
        InputRotation();
        Movement();
          
    }

    void Movement()
    {
        if(HareketTipi == MovementType.Strafe)
        {
            Anim.SetFloat("iX", inputX, delay, Time.deltaTime * 10);
            Anim.SetFloat("iY", inputY, delay, Time.deltaTime * 10);
            

            var hareketEdiyor = inputX != 0 || inputY != 0;

            if(hareketEdiyor)
            {
                float yawCamera = mainCam.transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), StrafeTurnSpeed * Time.fixedDeltaTime);
                Anim.SetBool("strafeMoving", true);
            }

            else
            {
                Anim.SetBool("strafeMoving", false);
            }

        }


        if (HareketTipi == MovementType.Directional)
        {
            StickDirection = new Vector3(inputX, 0, inputY);

            if (Input.GetKey(SprintButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, SprintFov, Time.deltaTime * 2);

                MaxSpeed = 2;
                inputX = 2 * Input.GetAxis("Horizontal");
                inputY = 2 * Input.GetAxis("Vertical");
            }
            else if (Input.GetKey(WalkButton))
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);

                MaxSpeed = 0.2f;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }
            else
            {
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, normalFov, Time.deltaTime * 2);

                MaxSpeed = 1;
                inputX = Input.GetAxis("Horizontal");
                inputY = Input.GetAxis("Vertical");
            }
        }












    }


    void InputMove()
    {
        Anim.SetFloat("speed", Vector3.ClampMagnitude(StickDirection, MaxSpeed).magnitude, delay, Time.deltaTime * 10);
    }

    void InputRotation()
    {
        Vector3 rotOfset = mainCam.transform.TransformDirection(StickDirection);
        rotOfset.y = 0;

        Model.forward = Vector3.Slerp(Model.forward, rotOfset, Time.deltaTime * rotationspeed);



    }
}
