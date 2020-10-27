using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Player
{
    void goLeft();
    void goRight();
    void goUp();
    void goDown();
}

public class EMGController : MonoBehaviour
{
    public MonoBehaviour player;
    public EMGInput input;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (input.getButtonInput(EMG.Flexion))
        {
            Debug.Log("Flexion Button Input!");
            Ball b = (Ball)player;
            b.goLeft();
        }
        else if (input.getButtonInput(EMG.Extension))
        {
            Debug.Log("Extension Button Input!");
            Ball b = (Ball)player;
            b.goRight();
        }
        else if (input.getButtonInput(EMG.Pronation))
        {
            Debug.Log("Pronation Button Input!");
            Ball b = (Ball)player;
            b.goUp();
        }
        else if (input.getButtonInput(EMG.Supination))
        {
            Debug.Log("Supination Button Input!");
            Ball b = (Ball)player;
            b.goDown();
        }
    }


}