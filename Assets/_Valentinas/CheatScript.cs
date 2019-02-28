using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatScript : MonoBehaviour
{
    // Start is called before the first frame update

    private List<string> cheatCode;

    void Start()
    {
        cheatCode = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && Input.GetKeyDown(KeyCode.I))
        {
            Resources.AddWood(500);
            Resources.AddStone(500);
            Resources.AddFood(500);
        }

        //if (Input.GetKey(KeyCode.C))
        //{
        //    cheatCode.Clear();
        //}


        //if (Input.GetKey(KeyCode.Y))
        //{
        //    cheatCode.Add("Y");
        //}

        //if (Input.GetKey(KeyCode.T))
        //{
        //    cheatCode.Add("T");
        //}

        //if(cheatCode.Count > 2)
        //{
        //    cheatCode.Clear();
        //}
        //else if(cheatCode.Count == 2)
        //{
        //    if(cheatCode.IndexOf("Y") == 0 && cheatCode.IndexOf("T") == 1)
        //    {
        //        Resources.AddWood(500);
        //        Resources.AddStone(500);
        //        Resources.AddFood(500);
        //        cheatCode.Clear();
        //    }
        //}

    }
}
