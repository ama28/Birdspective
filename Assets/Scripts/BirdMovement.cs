using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BirdMovement : MonoBehaviour
{

    //public CharacterController controller;
    //public GameObject player;
    private Rigidbody rb;
    private Transform transform_box;

    //movement stuff
    public float speed = 10f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //perspective stuff
    bool perspective;

    //Jump Stuff
    //public float gravity = 9.81f;
    //public float jumpSpeed = 3.5f;
    //private float dirY;
    //private bool canDoubleJump = false;
    //private float doubleJump = 0.5f;
    //public bool isGrounded;
    public float JumpForce = 5.0f;
    public float perspectiveJF = 500;


    //dash stuff
    public Vector3 direction;

    //heights time
    public float peak = 30;

    bool faceFront; 

    private void Start()
    {
        faceFront = true; 
        perspective = true;
        //player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody>();
        transform_box = GetComponent<Transform>();
    }

    void Update()
    {
        //restart scene stuff
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartScene();
        }

        if (transform.position.y < -5)
        {
           RestartScene(); 
        }

        //switch perspective
        if (Input.GetKeyDown(KeyCode.P))
        {
            perspective = !perspective;
            if(!perspective) 
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                StartCoroutine(WaitASecond());
            }
        }

        //jump stuff
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector3(0f, JumpForce, 0f), ForceMode.Impulse);
        }
    }

    void FixedUpdate() 
    {
        if (perspective)
        {

            //3d movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction == Vector3.zero)
            {
                return;
            }

            Quaternion rotate = Quaternion.LookRotation(direction);
            rotate = Quaternion.RotateTowards(
                transform.rotation,
                rotate,
                360 * Time.deltaTime
            );

            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
            rb.MoveRotation(rotate);
            //transform.LookAt(transform.position + direction);
        }
        else
        {
            float horizontal = Input.GetAxis("Horizontal");
            direction = new Vector3(0f, 0f, horizontal).normalized;
            transform.Rotate(0f, 0f, 0f);
            if (faceFront && horizontal < 0) {
                faceFront  = !faceFront;
                transform.Rotate(0f, 180f, 0f);
            }
            if (!faceFront && horizontal > 0) {
                faceFront  = !faceFront;
                transform.Rotate(0f, 180f, 0f);
            }
            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(0.7f);
        transform_box.position = new Vector3(transform.position.x, peak, transform.position.z);
        rb.AddForce(new Vector3(0f, -2.0f*JumpForce, 0f), ForceMode.Impulse);
    }
}

