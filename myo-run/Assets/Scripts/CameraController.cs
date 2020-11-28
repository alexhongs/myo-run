using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera to Follow the Player at offset'd distance
public class CameraController : MonoBehaviour
{
    private Transform player;

    private float yOffset = 6.96f;
    private float zOffset = 7.08f;

    private float yInitial = 7.14f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + yOffset, player.position.z - zOffset);

        // TODO: make camera slowly follow
        //if (player.position.y > 3.3f)
        //{
        //    transform.position = new Vector3(player.position.x, player.position.y + yOffset, player.position.z - zOffset);
        //}
        //else
        //{
        //    transform.position = new Vector3(player.position.x, yInitial, player.position.z - zOffset);
        //}
    }
}
