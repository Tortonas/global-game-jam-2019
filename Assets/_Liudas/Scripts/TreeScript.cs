using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : EntityScript
{
	public GameObject droppingTreePrefab;
    public Mesh tempMesh;
    MainGroundScript groundScr;
	BoxCollider[] boxColliders;

	private void Start()
	{
		groundScr = GameObject.FindGameObjectWithTag("GroundTag").GetComponent<MainGroundScript>();
		boxColliders = GetComponents<BoxCollider>();
		//groundScr.GenerateNavMeshes();
	}

	void DisableCapsuleCollider()
	{
		for (int i = 0; i < boxColliders.Length; i++)
		{
			boxColliders[i].enabled = false;
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
		/// TODO: spawn particles in drop tree die
		resourceGenerationScr.RemoveTree(gameObject);
		DisableCapsuleCollider();
        GameObject obj = Instantiate(droppingTreePrefab, transform.position, transform.rotation);
        obj.transform.GetChild(0).GetChild(0).GetComponent<MeshFilter>().mesh = tempMesh;
        groundScr.GenerateNavMeshes();
		Destroy(gameObject);
	}
}
