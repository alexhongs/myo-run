using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Player
{
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;
    GameObject playerParent;
    Rigidbody rb;
    Rigidbody parent_rb;
    Vector3 velocity;

    Animator animator;
    public Animator santaAnimator;

    float COLUMN_DISTANCE = 3.0f;
    float gravity = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        playerParent = GameObject.FindGameObjectWithTag("PlayerParent");
        rb = this.GetComponent<Rigidbody>();
        parent_rb = playerParent.GetComponent<Rigidbody>();
        parent_rb.velocity = new Vector3(0,0, movementSpeed);
        velocity = new Vector3(0, 0, movementSpeed);
        lane = 0;

        animator = this.GetComponent<Animator>();
        //santaAnimator = this.GetComponentInChildren<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            this.goLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            this.goRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.goUp();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.goDown();
        }
        //parent_rb.velocity = new Vector3(0, 0, movementSpeed);
        //if (!isGrounded)
        //{
        //    velocity.y -= (gravity * Time.deltaTime);
        //}

        //parent_rb.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpawnTrigger")
        {
            Debug.Log("Road Spawn Trigger Entered");
            spawnManager.SpawnTriggerEntered();
        }
        if(other.tag == "ObstacleRoad")
        {
            Debug.Log("Obstacle Road Entered");
            isGrounded = true;
            santaAnimator.SetTrigger("run");
            santaAnimator.ResetTrigger("jump");
        }
        //Debug.Log("Player Trigger Enter!");
    }

    public float horizontalSpeed = 10.0f;
    public float slide_duration = 0.35f;
    
    float RIGHT_LANE = 1.0f;
    float CENTER_LANE = 0.0f;
    float LEFT_LANE = -1.0f;

    float lane = 0; // left: -1, center: 0, right: 1
    
    public void goLeft()
    {
        Debug.Log("Player Left");
        if (lane == RIGHT_LANE || lane == CENTER_LANE)
        {
            lane -= 0.5f;
            //rb.velocity = new Vector3(-horizontalSpeed, 0, movementSpeed);
            velocity.x = -horizontalSpeed;
            velocity.z = movementSpeed;
            StartCoroutine(stopSlide(-0.5f));
        }

        // This code locks to lane, which is not preferable
        //float new_x = (this.transform.position.x == COLUMN_DISTANCE) ? 0 : -COLUMN_DISTANCE;
        //this.transform.position = new Vector3(new_x, this.transform.position.y, this.transform.position.z);
    }

    public void goRight()
    {
        Debug.Log("Player Right");
        if (lane == LEFT_LANE || lane == CENTER_LANE)
        {
            lane += 0.5f;
            //rb.velocity = new Vector3(horizontalSpeed, 0, movementSpeed);
            velocity.x = horizontalSpeed;
            velocity.z = movementSpeed;
            StartCoroutine(stopSlide(0.5f));
        }

        // This code locks to lane, which is not preferable
        //float new_x = (this.transform.position.x == -COLUMN_DISTANCE) ? 0 : COLUMN_DISTANCE;
        //this.transform.position = new Vector3(new_x, this.transform.position.y, this.transform.position.z);
    }

    float jumpForce = 13.0f;
    public bool isGrounded = true;
    public void goUp()
    {
        Debug.Log("Player Jump");
        if(isGrounded)
        {
            isGrounded = false;
            velocity.y = jumpForce;

            santaAnimator.SetTrigger("jump");
            santaAnimator.ResetTrigger("run");
        }
    }

    public void goDown()
    {
        Debug.Log("Player Slide");
        if (isGrounded)
        {
            animator.SetTrigger("slide");
        }
        
        //this.transform.position = new Vector3(-COLUMN_DISTANCE, this.transform.position.y, this.transform.position.z);
    }

    IEnumerator stopSlide(float lane_step)
    {
        yield return new WaitForSeconds(slide_duration);
        lane += lane_step;

        velocity.x = 0;
        velocity.z = movementSpeed;

        // Buggy: Aligns to center so that left and right movements are calibrated back
        if (lane == CENTER_LANE)
        {
            this.transform.position = new Vector3(0, this.transform.position.y, this.transform.position.z);
        }
    }

    //IEnumerator stopJump()
    //{
    //    yield return new WaitForSeconds(.75f);
    //    rb.velocity -= new Vector3(rb.velocity.x, -2*verticalVelocity, movementSpeed);
    //    yield return new WaitForSeconds(.75f);
    //    rb.velocity = new Vector3(rb.velocity.x, 0, movementSpeed);
    //}
}
