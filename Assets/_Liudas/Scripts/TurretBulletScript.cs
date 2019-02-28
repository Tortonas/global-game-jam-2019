using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletScript : MonoBehaviour
{
	float damage;
	public float speed = 10;
	EnemyScript enemyScr;

	public void GiveTarget(EnemyScript enemyS, float dmg)
	{
		enemyScr = enemyS;
		damage = dmg;
	}

	public void Update()
	{

		/// Bullet deletion could be cleaner
		if (enemyScr == null)
		{
			Destroy(gameObject);
		} else
		{
			transform.position = Vector3.MoveTowards(transform.position, enemyScr.transform.position, Time.deltaTime * speed);

			if (Vector3.Distance(transform.position, enemyScr.transform.position) < 0.2f)
			{
				enemyScr.ReceiveDamage(damage);
				Destroy(gameObject);
                GameObject go = Instantiate(enemyScr.GetComponent<Damageable>().ParticlePrefab, transform);
			}
		}


		

	}

}
