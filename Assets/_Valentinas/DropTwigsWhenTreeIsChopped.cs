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
        Invoke("generateTwigs", 0.9f);
    }

    void generateTwigs()
    {
        int twigCount = Random.Range(2, 4); //for ints

        for (int i = 0; i < twigCount; i++)
        {
            Instantiate(twigObject, GetRandomPos(gameObject.transform.position), gameObject.transform.rotation);
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.left, Time.deltaTime * 60);
    }

	Vector3 GetRandomPos(Vector3 currentPos)
	{
		var x = Random.Range(-1f, 1f);
		var z = Random.Range(-1f, 1f);

		return new Vector3(currentPos.x + x, 0, currentPos.z + z);
	}

}
