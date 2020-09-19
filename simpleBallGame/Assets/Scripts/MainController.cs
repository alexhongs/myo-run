using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float mag = 10f * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(-mag, 0f, 0f);
            print("Pressed Left");
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(mag, 0f, 0f);
            print("Pressed Right");
        }
        else if(Input.GetKey(KeyCode.UpArrow)) {
            transform.Translate(0f, mag, 0f);
            print("Pressed Up");
        }
        else if(Input.GetKey(KeyCode.DownArrow)) {
            transform.Translate(0, -mag, 0f);
            print("Pressed Down");
        }
    }
}
