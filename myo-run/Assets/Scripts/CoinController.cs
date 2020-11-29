using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public HashSet<GameObject> obstaclesToDestroy;
    private int numberOfObstaclesToDestroy = 20;
    public ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        obstaclesToDestroy = new HashSet<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            GameObject obj = other.gameObject;
            obstaclesToDestroy.Add(obj);
            obj.SetActive(false);
            Debug.Log("Coin Controller Hit! " + other.gameObject.name + "  and  " + obj.name + "  " + obstaclesToDestroy.Count);
            scoreManager.incrementGiftScore();
            DestroyObjectsWhenFullandRegenerate();
        }
        //Debug.Log("Player Trigger Enter!");
    }

    private void DestroyObjectsWhenFullandRegenerate()
    {
        if (obstaclesToDestroy.Count > numberOfObstaclesToDestroy)
        {
            // Destroy obstacles
            foreach (GameObject obj in obstaclesToDestroy)
            {
                Destroy(obj);
            }
            obstaclesToDestroy.Clear();
        }
    }
}
