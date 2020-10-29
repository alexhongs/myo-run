using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera to Follow the Player at offset'd distance
public class CameraController : MonoBehaviour
{
    private Transform player;

    private float yOffset = 5.1f;
    private float zOffset = 4.07f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, transform.position.y, player.position.z - zOffset);
    }
}
