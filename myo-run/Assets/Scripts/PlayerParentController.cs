using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParentController : MonoBehaviour
{
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player Collision Enter!");
        if (collision.collider.tag == "Road")
        {
            player.isGrounded = true;
            Debug.Log("Santa animator run triggered!");
            player.santaAnimator.SetTrigger("run");
            player.santaAnimator.ResetTrigger("jump");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpawnTrigger")
        {
            Debug.Log("Road Spawn Trigger Entered");
            player.spawnManager.SpawnTriggerEntered();
        }
        if (other.tag == "ObstacleRoad")
        {
            Debug.Log("Obstacle Road Entered");
            player.isGrounded = true;
            player.santaAnimator.SetTrigger("run");
            player.santaAnimator.ResetTrigger("jump");
        }
        //Debug.Log("Player Trigger Enter!");
    }
}
