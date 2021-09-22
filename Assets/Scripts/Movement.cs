using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;



public class Movement : MonoBehaviour
{
    //Classes - (references)
    public Component freeLookCam;
    public Transform cam;
    public Rigidbody rplayer;
    public InputProvider freelook_Input;
    public Animator animator;
    public player_Skills p_Skills;
    public Slider e_Slider;
    public Slider h_Slider;

    //floats
    public float speed = 10.0f;
    public float sprintModifier = 10.0f;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    public float p_Energy = 100.0f;

    //booleans
    bool sprint_Override;
    public bool isSprinting = false;


    // Use this for initialization
    void Start()
    {
        freelook_Input.setOverride(true);
    }

    private void OnGUI()
    {
        //Mouse camera movement mechanic
        if (Input.mouseScrollDelta.y > 0.1)
        {
            float q = freelook_Input.getRigVal(0);
            float w = freelook_Input.getRigVal(1);
            float e = freelook_Input.getRigVal(2);

            
            if(q < 16)
            {
                float o_rigVal = w + 0.25f;
                float rigVal = q + 0.25f;
                freelook_Input.setRigVal(rigVal, 0);
                freelook_Input.setRigVal(rigVal, 2);
                freelook_Input.setRigVal(o_rigVal, 1);

            }

            if (q < 16)
            {
                float o_rigVal = w + 0.25f;
                float rigVal = q + 0.25f;
                freelook_Input.setRigVal(rigVal, 0);
                freelook_Input.setRigVal(rigVal, 2);
                freelook_Input.setRigVal(o_rigVal, 1);

            }

        }

        //to subtract values
        if (Input.mouseScrollDelta.y < -0.1)
        {
            float q = freelook_Input.getRigVal(0);
            float w = freelook_Input.getRigVal(1);
            float e = freelook_Input.getRigVal(2);

            if (q > 3)
            {
                float o_rigVal = w - 0.25f;
                float rigVal = q - 0.25f;
                freelook_Input.setRigVal(rigVal, 0);
                freelook_Input.setRigVal(rigVal, 2);
                freelook_Input.setRigVal(o_rigVal, 1);

            }

            if (q > 3)
            {
                float o_rigVal = w - 0.25f;
                float rigVal = q - 0.25f;
                freelook_Input.setRigVal(rigVal, 0);
                freelook_Input.setRigVal(rigVal, 2);
                freelook_Input.setRigVal(o_rigVal, 1);

            }
        }

        if (Input.GetMouseButton(2))
        {
            freelook_Input.setOverride(false);
            freelook_Input.GetAxisCustom("Mouse X");
            freelook_Input.GetAxisCustom("Mouse Y");
            Cursor.lockState = CursorLockMode.Locked;

        }

        if ((Input.GetMouseButtonUp(2)) && (Cursor.lockState == CursorLockMode.Locked))
        {
            freelook_Input.setOverride(true);
            Cursor.lockState = CursorLockMode.None;
        }


    }

    // Update is called once per frame
    void Update()
    {

        //energy bar
        e_Slider.value = p_Energy;

        //punching
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("isPunching", true);
        }

        else
        {
            animator.SetBool("isPunching", false);
        }

        //energy mechanic
        if (isSprinting == false && p_Energy < 20)
        {
            sprint_Override = true;

        }

        else if(sprint_Override == true && p_Energy > 20)
        {
            sprint_Override = false;
        }

        if (isSprinting == false && p_Energy < 100.0f)
        {
            p_Energy = p_Energy + 0.023f;
        }

        

        //wasd movement code
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //part of sprinting mechanic
        if (direction.magnitude == 0)
        {
            isSprinting = false;
            animator.SetBool("isWalking", false);
        }

        //move in direction of the mouse mechanic
        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("isWalking", true);
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //part of sprint mechanic
            if(Input.GetKey(KeyCode.LeftShift) && sprint_Override == false && p_Energy > 0)
            {
                animator.SetBool("isRunning", true);
                p_Energy = p_Energy - 0.06f;
                isSprinting = true;
                transform.position += (moveDir.normalized * (speed + sprintModifier) * Time.deltaTime);
            }

            else
            {
                isSprinting = false;
                transform.position += (moveDir.normalized * speed * Time.deltaTime);
            }  
        
        }

        if (isSprinting == false)
        {
            animator.SetBool("isRunning", false);
        }

    }

    public float getEnergy()
    {
        return p_Energy;
    }
}