using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public float obstacleStartDistance = 20.0f;
    public List<GameObject> obstaclePrefabEasy;
    private float easyObstacleDistance = 40.0f;

    public List<GameObject> obstaclePrefabNormal;
    private float normalObstacleDistance = 40.0f;

    public List<GameObject> obstaclePrefabHard;
    private float hardObstacleDistance = 80.0f;
    //public List<GameObject> OBSTACLE_PREFAB_EASY;
    //public List<GameObject> OBSTACLE_PREFAB_EASY;

    private List<GameObject> train;

    public HashSet<GameObject> obstaclesToDestroy;
    private int numberOfObstaclesToDestroy = 5;

    // Start is called before the first frame update
    void Start()
    {
        train = new List<GameObject>();
        obstaclesToDestroy = new HashSet<GameObject>();

        GameObject startPrefab = obstaclePrefabEasy[0];
        train.Add(Instantiate(startPrefab, new Vector3(0, 0, obstacleStartDistance), Quaternion.identity));

        for (int i = 0; i < 20; i++)
        {
            // Get Random Prefab
            GameObject randomPrefab = getRandomPrefab();
            createObstacle(randomPrefab);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    GameObject getRandomPrefab()
    {
        var prefab_list = obstaclePrefabEasy;
        int level = Random.Range(0, 10);
        if (7 < level && level < 9)
        {
            prefab_list = obstaclePrefabNormal;
        }
        else if (9 <= level)
        {
            prefab_list = obstaclePrefabHard;

        }

        int index = Random.Range(0, prefab_list.Count);
        return prefab_list[index];
    }

    void createObstacle(GameObject prefab)
    {
        float distance = 100.0f;

        // Calculate position of next obstacle based on previous object's position and length
        GameObject lastObject = train[train.Count - 1];
        float lastObjectZ = lastObject.transform.position.z;


        if (lastObject.tag == "ObstacleEasy")
        {
            distance = easyObstacleDistance;
        }
        else if (lastObject.tag == "ObstacleNormal")
        {
            distance = normalObstacleDistance;
        }

        GameObject newObject = Instantiate(prefab, new Vector3(0, 0, lastObjectZ + distance), Quaternion.identity);
        //Debug.Log("last object's tag " + lastObject.tag + "  pos : " + lastObject.transform.position.z + "  new object  pos : " + newObject.transform.position.z + "  train: " + train.Count + "  obstacleEasy : " + );

        // Add to the train of obstacles
        train.Add(newObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            //Destroy(other.gameObject);

            // Destory the entier obstacle group to optimize destroying and synchronize  with obstacle creation
            // which is created in groups.
            GameObject obj = other.gameObject.transform.parent.gameObject;
            if (obj)
            {
                
                train.Remove(obj);

                obstaclesToDestroy.Add(obj);
                Debug.Log("ObstacleSpawner Hit! " + other.gameObject.name + "  and  " + obj.name + "  " +  obstaclesToDestroy.Count);
                
                DestroyObjectsWhenFullandRegenerate();
            }


            //createObstacle(getRandomPrefab());
        }
    }

    // TODO: Might need to make to do optimizations on game object destruction
    private void DestroyObjectsWhenFullandRegenerate()
    {
        Debug.Log("Checking to see if destroy obstacles"+ obstaclesToDestroy.Count + " / " + numberOfObstaclesToDestroy);

        if (obstaclesToDestroy.Count > numberOfObstaclesToDestroy)
        {
            // Destroy obstacles
            foreach (GameObject obj in obstaclesToDestroy)
            {
                Destroy(obj);
            }

            // Create new obstacles
            int N = obstaclesToDestroy.Count;
            for (int i = 0; i < N; i++)
            {
                // Get Random Prefab
                GameObject randomPrefab = getRandomPrefab();
                createObstacle(randomPrefab);
            }

            obstaclesToDestroy.Clear();
        }
    }
}
