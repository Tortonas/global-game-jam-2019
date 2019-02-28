using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomiseRotationScript : MonoBehaviour
{

	private void Start()
	{
		int rotY = Random.Range(0, 360);
		Vector3 newRotVec = new Vector3(0, rotY, 0);
		transform.rotation = Quaternion.Euler(newRotVec);
	}

}
