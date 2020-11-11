using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    RoadSpawner roadSpawner;
    //ObstacleSpawner obstacleSpawner;
    Transform camera;
    // Start is called before the first frame update


    void Start()
    {
        roadSpawner = GetComponent<RoadSpawner>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        //obstacleSpawner = GetComponent<ObstacleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = camera.position;
    }

    public void SpawnTriggerEntered()
    {
        roadSpawner.PopAndPushRoad();
    }
}
