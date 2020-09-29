using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMGController : MonoBehaviour
{
    public MonoBehaviour player;
    public void Do(EMG input)
    {
        Player p = (Player)player;
        if(input.Equals(EMG.Flexion))
        {
            p.goLeft();
        }
        else if (input.Equals(EMG.Extension))
        {
            p.goRight();
        }
        else if (input.Equals(EMG.Pronation))
        {
            p.goUp();
        }
        else if (input.Equals(EMG.Supination))
        {
            p.goDown();
        }
    }
}
