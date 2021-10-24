using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement : MonoBehaviour
{
    public GameObject player;
    public Vector3 init; 
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        init = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = init;
    }
}
