using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdMovement : MonoBehaviour
{

    public CharacterController controller;

    //movement stuff
    public float speed = 5f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //perspective stuff
    bool perspective;

    //Jump Stuff
    public float gravity = 9.81f;
    public float jumpSpeed = 3.5f;
    private float dirY;
    private bool canDoubleJump = false;
    private float doubleJump = 0.5f;


    //dash stuff
    public Vector3 direction;

    private void Start()
    {
        perspective = true;
    }

    void Update()
    {
        //restart scene stuff
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

        //switch perspective
        if (Input.GetKeyDown(KeyCode.P))
        {
            perspective = !perspective;
        }

        if (perspective)
        {
            //3d movement
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f) 
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                controller.Move(direction * speed * Time.deltaTime);
            }
        }
        else
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            direction = new Vector3(0f, 0f, horizontal).normalized;
            if (direction.magnitude >= 0.1f) 
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                controller.Move(direction * speed * Time.deltaTime);
            }
        }

        //jump stuff
        if (controller.isGrounded) 
        {
            canDoubleJump = true;
            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                dirY = jumpSpeed;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump) 
            {
                dirY = jumpSpeed * doubleJump;
                canDoubleJump = false;
            }
        }

        dirY -= gravity * Time.deltaTime;
        direction.y = dirY;
        controller.Move(direction * speed * Time.deltaTime);
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

