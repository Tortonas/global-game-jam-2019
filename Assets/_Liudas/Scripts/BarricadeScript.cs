using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeScript : EntityScript
{
	PlayerBuildingScript playerBuildingScr;
	MainGroundScript groundScr;

	CapsuleCollider[] capsuleColliders;

	public void SetUp(PlayerBuildingScript playerBuildingS)
	{
		groundScr = GameObject.FindGameObjectWithTag("GroundTag").GetComponent<MainGroundScript>();
		capsuleColliders = GetComponents<CapsuleCollider>();
		playerBuildingScr = playerBuildingS;
	}

	void DisableCapsuleCollider()
	{
		for (int i = 0; i < capsuleColliders.Length; i++)
		{
			capsuleColliders[i].enabled = false;
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
		print(transform.name + ": got dmg " + otherDamage);
		health -= otherDamage;

		if (IsDead())
		{
			Die();
		}
	}
	public override void Die()
	{
		/// TODO: Instantiate stone particles
		playerBuildingScr.barricades.Remove(this);
		DisableCapsuleCollider();
		groundScr.GenerateNavMeshes();
		Destroy(gameObject);
	}


}
