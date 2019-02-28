using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : EntityScript
{
	PlayerScript playerScr;
	PlayerBuildingScript playerBuildingScript;

	public GameObject bulletPrefab;
	public Transform bulletShootPosition;

	public int stoneCount;

	public void SetUp(PlayerBuildingScript playerBuildingScr, PlayerScript playerS, ResourceGeneration resourceGenerationS)
	{
		resourceGenerationScr = resourceGenerationS;
		playerBuildingScript = playerBuildingScr;
		playerScr = playerS;
		StartCoroutine(CheckForEnemies());
		print("Got all the scripts: " + playerBuildingScr + "::" + playerS);
	}

	IEnumerator CheckForEnemies()
	{
		while(true)
		{
			yield return new WaitForSeconds(fireRate);

			if (playerScr.isGameActive)
			{
				if (stoneCount > 0)
				{
					var target = FindTarget();
					if (target != null)
					{
						print("Shoot!@!");
						var bullet = Instantiate(bulletPrefab, bulletShootPosition);
						var bulletScr = bullet.GetComponent<TurretBulletScript>();
						bulletScr.GiveTarget(target, damage);

						transform.LookAt(target.transform);
						stoneCount--;
					}
				}
			}
		}
	}

	EnemyScript FindTarget()
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

		if (minDist < reachDistance)
			return enemyScript;
		else
			return null;


	}

	public override void DealDamage(EntityScript entity)
	{
		//print("?? whi is this trying to do dmg");
		return;
	}

	public override void Die()
	{
		if (IsDead())
		{
			/// TODO: delete this from the lists
			/// Spawn death particles
			playerBuildingScript.turrets.Remove(this);
			Destroy(gameObject);
		}
	}

	public override void ReceiveDamage(float otherDamage)
	{
		health -= otherDamage;

		if (IsDead())
		{
			Die();
		}

		/// TODO: instantiate blood particles
	}

    public void Reload()
    {
        stoneCount += 10;
    }

}
