using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour
{
    public Text distanceScoreUI;
    public Text giftScoreUI;
    Transform player;

    double giftScore = 0;
    double distanceScore = 0;

    double maxScore = 100000000000000;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerParent").transform;
        giftScore = 0;
        distanceScore = 0;
        giftScoreUI.text = giftScore.ToString();
    }

    public void incrementGiftScore()
    {
        giftScore += 1;
        giftScoreUI.text = giftScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        distanceScore = Math.Round(player.position.z * 59);
        if(distanceScore > maxScore)
        {
            distanceScore =  maxScore;
        }
        distanceScoreUI.text = (Math.Round(player.position.z*59)).ToString();
    }
}
