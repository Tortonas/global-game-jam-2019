using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainGroundScript : MonoBehaviour
{

	NavMeshSurface[] navMeshes;

	void Awake()
	{
		navMeshes = GetComponents<NavMeshSurface>();

		GenerateNavMeshes();
	}

	public void GenerateNavMeshes()
	{
		for (int i = 0; i < navMeshes.Length; i++)
		{
			navMeshes[i].BuildNavMesh();
		}
	}

	private void Update()
	{
		//print(navMeshes);
	}

}
