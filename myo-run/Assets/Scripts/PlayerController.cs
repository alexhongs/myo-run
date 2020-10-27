using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, Player
{
    public float movementSpeed = 10f;
    public SpawnManager spawnManager;

    public float COLUMN_DISTANCE = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Rigidbody>().velocity = new Vector3(0,0, movementSpeed);   
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
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player on trigger enter " + other.tag);

        if (other.tag == "SpawnTrigger")
        {
            spawnManager.SpawnTriggerEntered();
        }
    }

    public float velocity_x = 0.0f;

    public void goLeft()
    {
        Debug.Log("Player Left");
        //GetComponent<Rigidbody>().velocity = new Vector3(-2.0f, 0, movementSpeed);
        //StartCoroutine(stopSlide());
        //float new_x = (this.transform.position.x == COLUMN_DISTANCE) ? 0 : -COLUMN_DISTANCE;
        //this.transform.position = new Vector3(new_x, this.transform.position.y, this.transform.position.z);
    }

    public void goRight()
    {
        Debug.Log("Player Right");
        //GetComponent<Rigidbody>().velocity = new Vector3(2.0f, 0, movementSpeed);
        //StartCoroutine(stopSlide());
        //float new_x = (this.transform.position.x == -COLUMN_DISTANCE) ? 0 : COLUMN_DISTANCE;
        //this.transform.position = new Vector3(new_x, this.transform.position.y, this.transform.position.z);
    }

    public void goUp()
    {
        Debug.Log("Player Jump");
        //this.transform.position = new Vector3(-COLUMN_DISTANCE, this.transform.position.y, this.transform.position.z);
    }

    public void goDown()
    {
        Debug.Log("Player Slide");
        //this.transform.position = new Vector3(-COLUMN_DISTANCE, this.transform.position.y, this.transform.position.z);
    }

    IEnumerator stopSlide()
    {
        yield return new WaitForSeconds(.5f);
        velocity_x = 0.0f;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, movementSpeed);
    }
}
