using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public Camera topDownCam;
    public Animator topAnimator;

    public Camera sideCam;

    private BoxCollider birdCollider;

    // Start is called before the first frame update
    void Start()
    {
        // start game in top down view
        topDownCam.enabled = true;
        sideCam.enabled = false;

        topAnimator = topDownCam.GetComponent<Animator>();

        birdCollider = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // play animation depending on which camera is on
            if (topDownCam.enabled)
            {
                topAnimator.CrossFadeInFixedTime("ShiftToSide", 0);

                // wait a second and switch to side camera
                StartCoroutine(WaitAndSwitch());
            }
            else // sideCam.enabled
            {
                //switch back to top camera
                topDownCam.enabled = true;
                sideCam.enabled = false;

                topAnimator.CrossFadeInFixedTime("ShiftToTop", 0);

                //change collider back to 3D
                birdCollider.size = new Vector3(1.0f, 1.0f, 1.0f);
            }
        }
    }

    // Wait one second (length of animation) before switching to side camera
    // and extending collider to at least double width of stage
    IEnumerator WaitAndSwitch()
    {
        yield return new WaitForSeconds(1f);
        topDownCam.enabled = false;
        sideCam.enabled = true;
        birdCollider.size = new Vector3(100.0f, 1.0f, 1.0f);
    }
}
