using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDTests : MonoBehaviour
{
    Random random;

    private void Start()
    {
        random = new Random();
    }

    void Update()
    {
        //HUD.instance.SetScore(Random.Range(0, 8192));
        //HUD.instance.SetStone(Random.Range(0, 8192));
        //HUD.instance.SetWood(Random.Range(0, 8192));
    }
}
