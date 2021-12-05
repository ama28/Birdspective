using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour
{
    public GameObject player;
    public Vector3 init;
    public float z_distance = 3.0f;
    public float y_distance = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "MainCamera")
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + y_distance, player.transform.position.z - z_distance);
        else transform.position = new Vector3(transform.position.x, player.transform.position.y, player.transform.position.z);
    }
}
