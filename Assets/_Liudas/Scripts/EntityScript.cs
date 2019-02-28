using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityScript : MonoBehaviour
{
	public float maxHealth = 10;
	public float health = 10;
	public float damage = 1;
	public float movementSpeed = 5f;
	public float fireRate = 0.5f;
	public float reachDistance = 2;

	public bool isDead;
	public bool canAttack = true;

	protected ResourceGeneration resourceGenerationScr;

	public abstract void Die();
	public abstract void DealDamage(EntityScript entity);
	public abstract void ReceiveDamage(float otherDamage);

	public void DieAnimation()
	{
		isDead = true;
		print("Time: " + Time.time + "   DieAnim");
		//transform.Rotate(Vector3.left, Time.deltaTime * 500);
		Invoke("Die", 0.5f);
	}

	public void SetUp(ResourceGeneration resourceGen)
	{
		resourceGenerationScr = resourceGen;
	}

	public bool IsDead()
	{
		return (health <= 0);
	}


}
