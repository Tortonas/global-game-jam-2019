using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTwigsWhenTreeIsChopped : MonoBehaviour
{
    public GameObject twigObject;
    // Start is called before the first frame update

    void Start()
    {
        Destroy(gameObject, 1f); //susinaikins po 1s atspawnintas medis.
        invoke("generateTwigs", 0.9f);
    }

    void generateTwigs()
    {
        Random r = new Random();
        int twigCount = r.Next(2, 4); //for ints


        for (int i = 0; i < twigCuont; i++)
        {
            Instantiate(twigObject, gameObject.transform.position, gameObject.transform.rotation);
        }
    }

    void Update()
    {
        treeObject.transform.RotateAround(Vector3.left, Time.deltaTime * 5);
    }
}
