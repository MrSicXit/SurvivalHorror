using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //takes the movement of the mouse
    Vector2 mouseLook; //keep track of how much movement the camera has made
    Vector2 smoothV;
    Vector3 moveDirection = Vector3.zero;

    [SerializeField] CharacterController controller;
    [SerializeField] Transform cameraPosition;

    [SerializeField] float sensitivity = 5f;
    [SerializeField] float smoothing = 2f; //smooth down the movement of the mouse to make it less erratic
    [SerializeField] float minY = -90f;
    [SerializeField] float maxY = 90f;
    [SerializeField] float gravity = 20.0f;

    [SerializeField] float walkSpeed = 2.5f;
    [SerializeField] float runSpeed = 4f;
    [SerializeField] float jumpSpeed = 8.0f;
    [SerializeField] float runHeadBobbingMultiplier = 1.5f;
    [SerializeField] float headBobbingSpeed = 0.18f;
    [SerializeField] float headBobbingAmount = 0.2f;

    float waveslice = 0;
    float horizontal;
    float vertical;
    float translation;
    float straffe;

    float midpoint = 0.4f;
    float timer;
    float runBobbingAmount, runBobbingSpeed, walkBobbingAmount, walkBobbingSpeed;
    bool running = false;

    public bool paused;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

        if (!paused)
        {
            //take the mouse x and y values as they come in
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));

            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1 / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1 / smoothing);

            mouseLook += smoothV;
            mouseLook.y = Mathf.Clamp(mouseLook.y, minY, maxY);

            cameraPosition.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);

            //----------

            //horizontal = Input.GetAxis("Horizontal");
            //vertical = Input.GetAxis("Vertical");

            //translation = vertical * walkSpeed * Time.deltaTime;
            //straffe = horizontal * walkSpeed * Time.deltaTime;

            walkBobbingAmount = headBobbingAmount;
            walkBobbingSpeed = headBobbingSpeed;

            runBobbingAmount = headBobbingAmount * runHeadBobbingMultiplier;
            runBobbingSpeed = headBobbingSpeed * runHeadBobbingMultiplier;


            //if (Input.GetKey(KeyCode.LeftShift))
            //{
            //    running = true;
            //}
            //else
            //{
            //    running = false;
            //}

            //if (running)
            //{
            //    translation = vertical * runSpeed * Time.deltaTime;
            //    straffe = horizontal * runSpeed * Time.deltaTime;
            //}

            if (controller.isGrounded)
            {
                // We are grounded, so recalculate
                // move direction directly from axes

                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
                moveDirection *= walkSpeed;

                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }

            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);

            Headbobbing();
            transform.Translate(straffe, 0, translation);


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                Pausing();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

            }

        }
    }

    void Headbobbing()
    {

        Vector3 cSharpConversion = cameraPosition.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            if (running)
            {
                timer = timer + runBobbingSpeed;
            }
            else
            {
                timer = timer + walkBobbingSpeed;
            }
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }
        if (waveslice != 0)
        {
            float translateChange;
            if (running)
            {
                translateChange = waveslice * runBobbingAmount;
            }
            else
            {
                translateChange = waveslice * walkBobbingAmount;
            }
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.25f, 0.75f);
            translateChange = totalAxes * translateChange;
            cSharpConversion.y = midpoint + translateChange;
        }
        else
        {
            cSharpConversion.y = midpoint;
        }
        cameraPosition.localPosition = cSharpConversion;
    }

    public void Pausing()
    {
        paused = !paused;
    }
}
