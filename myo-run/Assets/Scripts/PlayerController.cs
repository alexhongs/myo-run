using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Player
{
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;
    GameObject playerParent;
    Rigidbody parent_rb;

    Animator animator;
    public Animator santaAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerParent = GameObject.FindGameObjectWithTag("PlayerParent");
        parent_rb = playerParent.GetComponent<Rigidbody>();
        parent_rb.velocity = new Vector3(0,0, movementSpeed);
        velocity = new Vector3(0, 0, movementSpeed);
        lane = 0;

        animator = this.GetComponent<Animator>();
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

        parent_rb.velocity = new Vector3(parent_rb.velocity.x, parent_rb.velocity.y, movementSpeed);
    }
    public float h_speed = 10.0f;
    private void FixedUpdate()
    {
        if (isLaneChanging)
        {
            Vector3 horizontalMove = transform.right * h_speed * Time.fixedDeltaTime;
            Vector3 newPosition = parent_rb.position + horizontalMove;
            if(newPosition.x > 3.7f)
            {
                isLaneChanging = false;
                lane += lane_step;
            }
            if(newPosition.x < -3.7f)
            {
                isLaneChanging = false;
                lane += lane_step;
            }
            parent_rb.MovePosition(parent_rb.position + horizontalMove);
        }
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
    bool isLaneChanging = false;
    float lane_step = 0.0f;

    public void goLeft()
    {
        Debug.Log("Player Left");
        if (lane == RIGHT_LANE || lane == CENTER_LANE)
        {
            lane_step = -0.5f;
            lane += lane_step;
            isLaneChanging = true;
            h_speed = -1 * Mathf.Abs(h_speed);
        }
    }

    public void goRight()
    {
        Debug.Log("Player Right");
        if (lane == LEFT_LANE || lane == CENTER_LANE)
        {
            lane_step = 0.5f;
            lane += lane_step;
            isLaneChanging = true;
            h_speed = Mathf.Abs(h_speed);
        }
    }

    public bool isGrounded = true;
    public void goUp()
    {
        Debug.Log("Player Jump");
        if(isGrounded)
        {
            isGrounded = false;
            //velocity.y = jumpForce;
            parent_rb.velocity = new Vector3(parent_rb.velocity.x, 11.0f, movementSpeed);
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
    }
}
