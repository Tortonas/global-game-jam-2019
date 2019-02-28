using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : EntityScript
{
	public GameObject stonePrefab;
	MainGroundScript groundScr;
	BoxCollider boxColliders;

	private void Start()
	{
		groundScr = GameObject.FindGameObjectWithTag("GroundTag").GetComponent<MainGroundScript>();
		boxColliders = GetComponent<BoxCollider>();
	}

	void DisableCapsuleCollider()
	{
		boxColliders.enabled = false;
	}


	void GenerateStones()
	{
		int stoneCount = Random.Range(2, 4); //for ints

		for (int i = 0; i < stoneCount; i++)
		{
			/// TODO: not gameObject.transform.position but in random circle
			Instantiate(stonePrefab, GetRandomPos(gameObject.transform.position), gameObject.transform.rotation);
		}
	}

	public override void DealDamage(EntityScript otherEntity)
	{
		if (otherEntity == null)
		{
			print("Entity is null");
			return;
		}

		otherEntity.ReceiveDamage(damage);
	}
	public override void ReceiveDamage(float otherDamage)
	{
		health -= otherDamage;

		if (IsDead())
		{
			Die();
		}
	}
	public override void Die()
	{
		print(resourceGenerationScr);
		/// TODO: spawn particles stone dissappear
		GenerateStones();
		resourceGenerationScr.RemoveStone(gameObject);
		DisableCapsuleCollider();
		//Instantiate(droppingTreePrefab, transform.position, transform.rotation);
		groundScr.GenerateNavMeshes();
		Destroy(gameObject);
	}

	Vector3 GetRandomPos(Vector3 currentPos)
	{
		var x = Random.Range(-1f, 1f);
		var z = Random.Range(-1f, 1f);

		return new Vector3(currentPos.x + x, 0, currentPos.z + z);
	}

}
