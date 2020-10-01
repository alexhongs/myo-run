using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

class Ball : MonoBehaviour, Player
{
    float distance = 1f;

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
}