using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Spawn
{
    public GameObject[] gameObject;
    public int spawnCount = 1;
    public int index;
    public bool oneTime = false;
}
public class ResourceGeneration : MonoBehaviour
{
	public PlayerBuildingScript buildingScr;
    public Camera lightCamera;
    public GameObject treesParent;
    public GameObject rocksParent;
    public GameObject enemiesParent;
    public Spawn[] resources;
    public List<GameObject> rocks;
    public List<GameObject> trees;
    public List<EnemyScript> animals;
    public MainGroundScript mainGroundScr;
    public float xRange = 100;
    public float zRange = 100;
    public int generate = 1;
    float time = 0;
    int days = 0;
    float day = 0;
    void Start()
    {
        days = 0;
        List<GameObject> trees = new List<GameObject>();
        List<GameObject> rocks = new List<GameObject>();
        List<EnemyScript> animals = new List<EnemyScript>();
        foreach (Spawn resource in resources)
        {
            if (resource.index < 2)
            {
                for (int i = 0; i < resource.spawnCount; i++)
                {
                    Generate(resource, i, true);
                }
            }
        }
        mainGroundScr.GenerateNavMeshes();

    }

    // Update is called once per frame
    void Update()
    {
        day = lightCamera.GetComponent<DayNight>().timeOfDay;

        if (day == 1)
        {
            generate = 0;
            days++;
        }
        if (day == 0 && generate < 1 && days > 0)
        {
            foreach (Spawn resource in resources)
            {

                if (!resource.oneTime)
                {
                    int length = resource.spawnCount;
                    if (resource.index == 0)
                    {
                        length -= trees.Count;
                    }
                    else if (resource.index == 1)
                    {
                        length -= rocks.Count;
                    }
                    else if (resource.index > 1)
                    {
                        resource.spawnCount += 1;
                        length = resource.spawnCount;
                    }
                    for (int i = 0; i < length; i++)
                    {
                        Generate(resource, i, true);
                    }
                }

            }
            mainGroundScr.GenerateNavMeshes();
            generate = 1;
        }


    }

    void Generate(Spawn resource, int i, bool addTo)
    {

        float xCord = Random.Range(-xRange, xRange);
        float zCord = Random.Range(-zRange, zRange);
        bool check = true;
        int ranIndex = Random.Range(0,resource.gameObject.Length-1);
        while (check)
        {
            xCord = Random.Range(-xRange, xRange);
            zCord = Random.Range(-zRange, zRange);
            Vector3 viewPos = Camera.main.WorldToViewportPoint(new Vector3(xCord, 0, zCord));
            if (raycast(xCord, 40, zCord))
            {
                check = true;
            }
            else if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
            {
                check = true;
            }
            else if (!(viewPos.x >= -0 && viewPos.x <= 1.01 && viewPos.y >= -0 && viewPos.y <= 1 && viewPos.z > -0) && !raycast(xCord, 40, zCord))
            {
                check = false;
            }


        }
        GameObject obj = Instantiate(resource.gameObject[ranIndex], new Vector3(xCord, 0, zCord), Quaternion.identity);
        if (addTo)
        {
            if (resource.index == 0)
            {
                obj.GetComponent<EntityScript>().SetUp(this);
                obj.transform.parent = treesParent.transform;
                if (!resource.oneTime)
                {
                    trees.Add(obj);
                }
            }
            else if (resource.index == 1)
            {
                if (obj.GetComponent<EntityScript>() != null)
                {
                    obj.GetComponent<EntityScript>().SetUp(this);
                }
                obj.transform.parent = rocksParent.transform;
                if (!resource.oneTime)
                {
                    rocks.Add(obj);
                }
            }
            else if (resource.index > 1)
            {
                obj.GetComponent<EnemyScript>().SetUpAnimal(this, mainGroundScr, buildingScr);
                obj.transform.parent = enemiesParent.transform;
                animals.Add(obj.GetComponent<EnemyScript>());
            }
        }
    }

    bool raycast(float x, float y, float z)
    {
        //Debug.DrawRay(new Vector3(x, y, z),Vector3.down);
        RaycastHit hit;
        float RayCastRange = 50;
        Ray ray = new Ray(new Vector3(x, y, z), transform.TransformDirection(Vector3.down));
        if (Physics.Raycast(ray, out hit, RayCastRange))
        {
            if (hit.collider.tag != "GroundTag")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    public void RemoveTree(GameObject obje)
    {
        trees.Remove(obje);
    }
    public void RemoveStone(GameObject obje)
    {
        rocks.Remove(obje);
    }
}
