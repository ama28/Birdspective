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
    public float JumpForce = 5;
    public float perspectiveJF = 500;


    //dash stuff
    public Vector3 direction;

    private void Start()
    {
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

        //switch perspective
        if (Input.GetKeyDown(KeyCode.P))
        {
            perspective = !perspective;
            //if (Mathf.Abs(rb.velocity.y) < 0.001f)
            //{
            //    rb.AddForce(new Vector3(0f, perspectiveJF, 0f), ForceMode.Impulse);
            //}
        }

        if (perspective)
        {
            //3d movement
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            direction = new Vector3(horizontal, 0f, vertical);

            rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
            transform.LookAt(transform.position + direction);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            float horizontal = Input.GetAxis("Horizontal");
            direction = new Vector3(0f, 0f, horizontal).normalized;
            rb.MovePosition(transform.position + direction * (speed * 2) * Time.deltaTime);
        }

        //jump stuff
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            rb.AddForce(new Vector3(0f, JumpForce, 0f), ForceMode.Impulse);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

