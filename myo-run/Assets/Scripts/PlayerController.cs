using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Player
{
    public float movementSpeed = 2f;
    public SpawnManager spawnManager;
    GameObject playerParent;
    Rigidbody parent_rb;
    Vector3 velocity;

    Animator animator;
    public Animator santaAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerParent = GameObject.FindGameObjectWithTag("PlayerParent");
        parent_rb = playerParent.GetComponent<Rigidbody>();
        parent_rb.velocity = new Vector3(0,0, movementSpeed);
        velocity = new Vector3(0, 0, movementSpeed);
        animator = this.GetComponent<Animator>();
        isLaneChanging = false;
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

        parent_rb.velocity = new Vector3(parent_rb.velocity.x, parent_rb.velocity.y, 8f);
    }
    public float h_speed = 10.0f;
    private void FixedUpdate()
    {
        if (isLaneChanging)
        {
            Vector3 horizontalMove = transform.right * h_speed * Time.fixedDeltaTime;
            Vector3 newPosition = parent_rb.position + horizontalMove;
            if(newPosition.x > 3.7f && next_lane == RIGHT_LANE)
            {
                previous_lane = next_lane;
                isLaneChanging = false;
                previousX = parent_rb.position.x;
                previousY = parent_rb.position.y;
                savePreviousXY();
            }
            else if(Mathf.Abs(newPosition.x) < 0.05f && next_lane == CENTER_LANE)
            {
                previous_lane = next_lane;
                isLaneChanging = false;
                savePreviousXY();
            }
            else if(newPosition.x < -3.7f && next_lane == LEFT_LANE)
            {
                isLaneChanging = false;
                previous_lane = next_lane;
                savePreviousXY();
            }
            else if(next_lane == CENTER_LANE && previous_lane == LEFT_LANE && newPosition.x > 0.1f )
            {
                isLaneChanging = false;
                previous_lane = next_lane;
                savePreviousXY();
            }
            else if (next_lane == CENTER_LANE && previous_lane == RIGHT_LANE && newPosition.x < -0.1f)
            {
                isLaneChanging = false;
                previous_lane = next_lane;
                savePreviousXY();
            }
            parent_rb.MovePosition(parent_rb.position + horizontalMove);

            if (firstChange && (previousX != parent_rb.position.x || previousY != parent_rb.position.y))
            {
                firstChange = false;
                log(1);
            }
        }
    }
    float previousX = 0.0f;
    float previousY = 0.0f;
    void savePreviousXY()
    {
        previousX = parent_rb.position.x;
        previousY = parent_rb.position.y;
        //firstChange = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpawnTrigger")
        {
            spawnManager.SpawnTriggerEntered();
        }
        if(other.tag == "ObstacleRoad")
        {
            isGrounded = true;
            santaAnimator.SetTrigger("run");
            santaAnimator.ResetTrigger("jump");
        }
    }

    public float horizontalSpeed = 10.0f;
    public float slide_duration = 0.35f;
    
    float RIGHT_LANE = 1.0f;
    float CENTER_LANE = 0.0f;
    float LEFT_LANE = -1.0f;

    float next_lane = 0; // left: -1, center: 0, right: 1
    float previous_lane = 0;
    bool isLaneChanging = false;

    public bool firstChange = true;
    public void goLeft()
    {
        if (previous_lane == CENTER_LANE || previous_lane == RIGHT_LANE)
        {
            if (previous_lane == CENTER_LANE && next_lane == RIGHT_LANE)
            {
                next_lane = CENTER_LANE;
                previous_lane = RIGHT_LANE;
            }

            if (previous_lane == CENTER_LANE)
            {
                next_lane = LEFT_LANE;
            }
            else if (previous_lane == RIGHT_LANE)
            {
                next_lane = CENTER_LANE;
            }
            if(!isLaneChanging)
            {
                log(1);
                isLaneChanging = true;
            }
            //isLaneChanging = true;
            h_speed = -1 * Mathf.Abs(h_speed);
            previousX = parent_rb.position.x;
            previousY = parent_rb.position.y;
        }
    }

    public void goRight()
    {
        if (previous_lane == CENTER_LANE || previous_lane == LEFT_LANE)
        {
            if(previous_lane == CENTER_LANE && next_lane == LEFT_LANE)
            {
                next_lane = CENTER_LANE;
                previous_lane = LEFT_LANE;
            }
            
            if (previous_lane == CENTER_LANE)
            {
                next_lane = RIGHT_LANE;
            }
            else if (previous_lane == LEFT_LANE)
            {
                next_lane = CENTER_LANE;
            }
            if (!isLaneChanging)
            {
                log(1);
                isLaneChanging = true;
            }
            //isLaneChanging = true;
            h_speed = Mathf.Abs(h_speed);
            previousX = parent_rb.position.x;
            previousY = parent_rb.position.y;
        }
    }

    public bool isGrounded = true;
    public void goUp()
    {
        //Debug.Log("Player Jump");
        if(isGrounded)
        {
            isGrounded = false;
            //velocity.y = jumpForce;
            parent_rb.velocity = new Vector3(parent_rb.velocity.x, 11.0f, movementSpeed);
            santaAnimator.SetTrigger("jump");
            santaAnimator.ResetTrigger("run");
            log(0);
        }
    }

    public void goDown()
    {
        //Debug.Log("Player Slide");
        if (isGrounded)
        {
            animator.SetTrigger("slide");
            log(0);
        }        
    }
    int logNumber = 1;
    void log(int isHorizontal)
    {
        //if(isHorizontal == 1 && !firstChange)
        //{
        //    Debug.Log(logNumber.ToString() + " : " + DateTime.UtcNow.ToString("hh.mm.ss.ffffff"));
        //    logNumber++;
        //    firstChange = true;
        //}
        //else
        //{
        //    Debug.Log(logNumber.ToString() + " : " + DateTime.UtcNow.ToString("hh.mm.ss.ffffff"));
        //    logNumber++;
        //}
        Debug.Log(logNumber.ToString() + " : " + DateTime.UtcNow.ToString("hh.mm.ss.ffffff"));
        logNumber++;
        //Debug.Log(DateTimeOffset.Now.ToUnixTimeMilliseconds());
    }
}
