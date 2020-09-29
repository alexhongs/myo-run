using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Ball : MonoBehaviour, Player
{
    float distance = 10f;
    public void goLeft()
    {
        transform.Translate(-distance, 0f, 0f);
    }

    public void goRight()
    {
        transform.Translate(distance, 0f, 0f);
    }

    public void goUp()
    {
        transform.Translate(0f, distance, 0f);
    }

    public void goDown()
    {
        transform.Translate(0f, -distance, 0f);
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}