using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    Vector3 init_pos;
    public Transform transform_box;

    private Rigidbody rb;

    bool perspective;

    public float MovementSpeed = 10;
    public float JumpForce = 5;

    private void Start()
    {
        perspective = true;
        rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        init_pos = transform_box.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            perspective = !perspective;
        }

        if (perspective)
        {
            MoveLR();
            MoveFB();
            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
            }
        }
        else
        {
            var movement2D = Input.GetAxis("Horizontal");
            transform.position += new Vector3(0, 0, movement2D) * Time.deltaTime * MovementSpeed;

            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
            }
        }
    }

    void MoveLR()
    {
        Vector3 vec_l = Vector3.zero;
        vec_l.x = Input.GetAxis("Horizontal");
        Vector3 v = new Vector3(vec_l.x, 0.0f, 0.0f) * Time.deltaTime * 5.0f;
        transform_box.Translate(v, Space.Self);
    }

    void MoveFB()
    {
        Vector3 vec_f = Vector3.zero;
        vec_f.z = Input.GetAxis("Vertical");
        Vector3 v = new Vector3(0.0f, 0.0f, vec_f.z) * Time.deltaTime * 5.0f;
        transform_box.Translate(v, Space.Self);
    }
}