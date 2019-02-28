using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingScript : MonoBehaviour
{
	public PlayerScript playerScr;
	public ResourceGeneration resourceGenerationScr;

	public GameObject barricadeModel;
    public GameObject turretModel;
    public GameObject trapModel;

    public Camera camera;
    public Transform playerModel;
    public float maximumRange;
    public float minimumRange;
    private RaycastHit rayInfo;
    public List<BarricadeScript> barricades;
    public List<TurretScript> turrets;

	SpriteRenderer tempRenderedOfCircleObject;


    public string whatAmIBuilding; // gali buti "Barricade" "Trap" "Turret"
	public bool amIInBuildingMenu; //sita valdo Domo scriptas
    public GameObject circleObject; //kazka idet reiks

    // Start is called before the first frame update
    void Start()
    {
        
        barricades = new List<BarricadeScript>();
        turrets = new List<TurretScript>();
		tempRenderedOfCircleObject = circleObject.GetComponent<SpriteRenderer>();
	}

    // Update is called once per frame
    void Update()
    {
        if (!amIInBuildingMenu)
        {
            circleObject.SetActive(false);
            //jeigu isjungtas building menu
            return;
        }
        else
        {
            circleObject.SetActive(true);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                var rayStartPosition = hit.point;
                rayStartPosition.y = 20;

                Debug.DrawRay(rayStartPosition, Vector3.down * 50, Color.black, 5f);

                bool shouldISpawnBarricade = true;

                Vector3 playerPositionTemp = new Vector3(playerModel.transform.position.x, 0f, playerModel.transform.position.z);
                Vector3 clickPositionTemp = new Vector3(hit.point.x, 0f, hit.point.z);

                Vector3 vectorRange = playerPositionTemp - clickPositionTemp;



                //kad nestatytu per toli
                clickPositionTemp.y = 0.1f;

                if (hit.transform.tag != "GroundTag")
                {
                    circleObject.transform.position = clickPositionTemp;
                    tempRenderedOfCircleObject.color = new Color(255f, 0f, 0f);
                    Debug.Log("Draudzia statyti ant ne ant zemes");
                }
                else if (vectorRange.magnitude > maximumRange || vectorRange.magnitude < minimumRange)
                {

					print(string.Format("{0}  {1}  {2}" ,vectorRange.magnitude, maximumRange, minimumRange));
                    //raudonas
                    circleObject.transform.position = clickPositionTemp;
                    tempRenderedOfCircleObject.color = new Color(255f, 0f, 0f);
                    Debug.Log("Pirmas");
                }
                else if(whatAmIBuilding == "Barricade") //barikados statymas
                {
                    //kreipiuos ar turiu resursu
                    bool doIHaveResources = CraftingSystem.CanCraftBarricade();
                    
                    tempRenderedOfCircleObject.color = new Color(0f, 0f, 255f);

                    if (doIHaveResources)
                    {
                        //melynas
                        tempRenderedOfCircleObject.color = new Color(0f, 0f, 255f);
                    }
                    else
                    {
                        //raudonas
                        tempRenderedOfCircleObject.color = new Color(255f, 0f, 0f);
                    }

                    circleObject.transform.position = clickPositionTemp;
                }
                else if (whatAmIBuilding == "Turret")
                {
                    bool doIHaveResources = CraftingSystem.CanCraftTurret();
                    if(doIHaveResources)
                    {
                        //melynas
                        tempRenderedOfCircleObject.color = new Color(0f, 0f, 255f);
                    }
                    else
                    {
                        //raudonas
                        tempRenderedOfCircleObject.color = new Color(255f, 0f, 0f);
                    }
                    circleObject.transform.position = clickPositionTemp;
                    
                    //melynas
                }
                else if (whatAmIBuilding == "Trap")
                {
                    bool doIHaveResources = CraftingSystem.CanCraftTrap();
                    if (doIHaveResources)
                    {
                        //melynas
                        tempRenderedOfCircleObject.color = new Color(0f, 0f, 255f);
                    }
                    else
                    {
                        //raudonas
                        tempRenderedOfCircleObject.color = new Color(255f, 0f, 0f);
                    }
                    circleObject.transform.position = clickPositionTemp;

                    //melynas
                }
            }
        }
    }

    public bool BuildBarricade()
    {
        RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            var rayStartPosition = hit.point;
            rayStartPosition.y = 20;

            Debug.DrawRay(rayStartPosition, Vector3.down * 50, Color.black, 5f);

            bool shouldISpawnBarricade = true;

            Vector3 playerPositionTemp = new Vector3(playerModel.transform.position.x, 0f, playerModel.transform.position.z);
            Vector3 clickPositionTemp = new Vector3(hit.point.x, 0f, hit.point.z);

            Vector3 vectorRange = playerPositionTemp - clickPositionTemp;

            //kad nestatytu per toli
            if (vectorRange.magnitude > maximumRange)
                shouldISpawnBarricade = false;

            //kad nestatytu per arti
            if (vectorRange.magnitude < minimumRange)
                shouldISpawnBarricade = false;


            //chekina ar yra range ir ar nestato ant kitos barikados ar ant kito turreto
            if (hit.transform.tag == "GroundTag" && shouldISpawnBarricade)
            {
                GameObject temp = Instantiate(barricadeModel, hit.point, Quaternion.identity);
				var scr = temp.GetComponent<BarricadeScript>();
				scr.SetUp(this);
				barricades.Add(scr);
				return true;
            }
		}
        return false;
	}

    public bool BuildTurret()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            var rayStartPosition = hit.point;
            rayStartPosition.y = 20;

            Debug.DrawRay(rayStartPosition, Vector3.down * 50, Color.black, 5f);
            print(hit.transform.name);

            bool shouldISpawnBarricade = true;

            Vector3 playerPositionTemp = new Vector3(playerModel.transform.position.x, 0f, playerModel.transform.position.z);
            Vector3 clickPositionTemp = new Vector3(hit.point.x, 0f, hit.point.z);

            Vector3 vectorRange = playerPositionTemp - clickPositionTemp;

            Debug.Log(vectorRange.magnitude);

            //kad nestatytu per toli
            if (vectorRange.magnitude > maximumRange)
                shouldISpawnBarricade = false;

            //kad nestatytu per arti
            if (vectorRange.magnitude < minimumRange)
                shouldISpawnBarricade = false;


            //chekina ar yra range ir ar nestato ant kitos barikados ar ant kito turreto
            if (hit.transform.tag == "GroundTag" && shouldISpawnBarricade)
            {
                GameObject temp = Instantiate(turretModel, hit.point, Quaternion.identity);
				var turretScript = temp.GetComponent<TurretScript>();
				turretScript.SetUp(this, playerScr, resourceGenerationScr);
				turrets.Add(turretScript);
                return true;
            }
        }
        return false;
    }

    public bool BuildTrap()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            var rayStartPosition = hit.point;
            rayStartPosition.y = 20;

            Debug.DrawRay(rayStartPosition, Vector3.down * 50, Color.black, 5f);
            print(hit.transform.name);

            bool shouldISpawnBarricade = true;

            Vector3 playerPositionTemp = new Vector3(playerModel.transform.position.x, 0f, playerModel.transform.position.z);
            Vector3 clickPositionTemp = new Vector3(hit.point.x, 0f, hit.point.z);

            Vector3 vectorRange = playerPositionTemp - clickPositionTemp;

            Debug.Log(vectorRange.magnitude);

            //kad nestatytu per toli
            if (vectorRange.magnitude > maximumRange)
                shouldISpawnBarricade = false;

            //kad nestatytu per arti
            if (vectorRange.magnitude < minimumRange)
                shouldISpawnBarricade = false;


            //chekina ar yra range ir ar nestato ant kitos barikados ar ant kito turreto
            if (hit.transform.tag == "GroundTag" && shouldISpawnBarricade)
            {
                GameObject temp = Instantiate(trapModel, hit.point, Quaternion.identity);
                //var trapScript = temp.GetComponent<TrapScript>();
                //trapScript.SetUp(this, playerScr, resourceGenerationScr);
               // turrets.Add(turretScript);
                return true;
            }
        }
        return false;
    }
}
