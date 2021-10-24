using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BirdDash : MonoBehaviour {

    BirdMovement moveScript;

    public float dashSpeed;
    public float dashTime = 0.25f;

    private void Start()
    {
        moveScript = GetComponent<BirdMovement>();
    }

    void Update () 
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash() 
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime) 
        {
            moveScript.controller.Move(moveScript.direction * dashSpeed * Time.deltaTime);

            yield return null;
        }

    }
 }