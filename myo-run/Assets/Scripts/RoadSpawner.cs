using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Road generator looping 7 Road Segments
public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    private float offset = 99.2f;

    // Start is called before the first frame update
    void Start()
    {
        if(roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(road => road.transform.position.z).ToList();
        }   
    }

    public void PopAndPushRoad()
    {
        GameObject road = roads[0];
        roads.Remove(road);
        float newZ = roads[roads.Count - 1].transform.position.z + offset;
        road.transform.position = new Vector3(0, 0, newZ);
        roads.Add(road);
    }
}
