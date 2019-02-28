using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingIndicatorMenu : MonoBehaviour
{
    
    public bool amIInBuildingMenu; //sita valdo Domo scriptas
    private PlayerScript playerObject; //sitas auto susiras
    public GameObject circleObject; //kazka idet reiks

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!amIInBuildingMenu)
        {
            //jeigu isjungtas building menu
            return;
        }
        else
        {
            //jeigu ijungtas building menu, rodys melyna circle
        }
    }
}
