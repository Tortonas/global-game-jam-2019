using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{

	public float damage = 5;
	public float reachDistance = 0.5f;

	ResourceGeneration resourceGenerationScr;

	public void Start()
	{
		resourceGenerationScr = GameObject.Find("ResourceSpawn").GetComponent<ResourceGeneration>();
		InvokeRepeating("SlowUpdate", 0.15f, 0.15f);
	}

	void SlowUpdate()
	{

		var closestAnimal = FindTarget(reachDistance);
		if (closestAnimal != null)
		{
			closestAnimal.ReceiveDamage(damage);
			Destroy(gameObject);
		}
		

	}

	EnemyScript FindTarget(float maxDistance)
	{
		EnemyScript enemyScript = null;
		float minDist = 50;

		print(resourceGenerationScr);

		var animalCount = resourceGenerationScr.animals.Count;
		for (int i = 0; i < animalCount; i++)
		{
			var animal = resourceGenerationScr.animals[i];
			var pos1 = animal.transform.position;
			var pos2 = transform.position;

			var dist = Vector3.Distance(pos1, pos2);
			if (dist < minDist)
			{
				minDist = dist;
				enemyScript = animal;
			}
		}

		print("Min dist is: " + minDist);

		if (minDist > maxDistance)
			return null;

		if (minDist < reachDistance)
			return enemyScript;
		else
			return null;


	}

}
