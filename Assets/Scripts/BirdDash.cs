using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BirdDash : MonoBehaviour {
 
    public Vector3 moveDirection;
 
    public const float maxDashTime = 1.0f;
    public float dashDistance = 10;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    public float dashSpeed = 20;
    private Rigidbody rb;

    public GameObject dashEffect;
    private ParticleSystem partSystem;

    bool dash = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        partSystem = dashEffect.GetComponent<ParticleSystem>();
    }

    void Update () 
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            currentDashTime = 0;  
            dash = true; 
        }
        if(currentDashTime < maxDashTime)
        {
            moveDirection = transform.forward * dashDistance;
            currentDashTime += dashStoppingSpeed;
            if (dash) {
                GameObject effect = Instantiate(dashEffect, transform.position, dashEffect.transform.rotation);
                partSystem.Play();
                var em = partSystem.emission; em.enabled = true;
                dash = false;
            }
        }
        else
        {
            moveDirection = Vector3.zero;
        }
        rb.AddForce(moveDirection * Time.deltaTime * dashSpeed, ForceMode.Impulse);
    }
 }