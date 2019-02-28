using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : EntityScript
{

	public GameObject dieParticlesPrefab;
	public GameObject meatPrefab;

	public string playerTag = "PlayerTag";
	public PlayerScript playerScr;

	public EntityScript target;

	PlayerBuildingScript buildingScr;
	MainGroundScript groundScr;

	NavMeshAgent agent;

	Transform player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag(playerTag).transform;
		playerScr = player.GetComponent<PlayerScript>();

		agent = GetComponent<NavMeshAgent>();
		agent.speed = movementSpeed;

		float startTime = Random.Range(0, 0.2f);
		InvokeRepeating("SlowUpdate", startTime, 0.2f);
	}

	private void Update()
	{
		/// TODO: check if path is valid

	}

	void SlowUpdate()
	{
		if (!playerScr.isGameActive || isDead)
			return;

		switch (agent.pathStatus)
		{
			case NavMeshPathStatus.PathInvalid:
				print("TELL LIUDAS  @@@@");
				break;

			case NavMeshPathStatus.PathComplete:
				target = player.GetComponent<EntityScript>();
				break;

			case NavMeshPathStatus.PathPartial:
				/// TODO: check nearest barricade, if in range attack it,
				/// after barricade is destroyed rebake navmesh
				target = FindClosestBarricade();
				break;

		}

		if (target == null)
		{
			print("Target is null");
			return;
		}

		if (canAttack)
		{
			if (Vector3.Distance(transform.position, target.transform.position) < reachDistance)
			{
				print("Mes galim attakuot!");
				target.ReceiveDamage(damage);
				Invoke("ResetCanAttack", fireRate);
				/// TODO: call attack animation
				agent.ResetPath();
				canAttack = false;
			}
		}

		if (canAttack)
			agent.SetDestination(target.transform.position);

	}

	void ResetCanAttack()
	{
		canAttack = true;
	}

	EntityScript FindClosestBarricade()
	{
		EntityScript barricadeScr = null;
		float minDist = 50;

		for (int i = 0; i < buildingScr.barricades.Count; i++)
		{
			var barricade = buildingScr.barricades[0];
			var pos1 = barricade.transform.position;
			var pos2 = transform.position;

			var dist = Vector3.Distance(pos1, pos2);
			if (dist < minDist)
			{
				minDist = dist;
				barricadeScr = barricade;
			}
		}

		if (minDist < reachDistance)
		{
			print("Radom kazka: " + barricadeScr);
			return barricadeScr;
		}
		else
		{
			print("Nepakako reach distance");
			return null;

		}


	}

	public void SetUpAnimal(ResourceGeneration resourceGen, MainGroundScript groundS, PlayerBuildingScript buildingS)
	{
		buildingScr = buildingS;
		resourceGenerationScr = resourceGen;
		groundScr = groundS;
	}

	public override void DealDamage(EntityScript entity)
	{
		if (entity == null)
		{
			print("Entity is null");
			return;
		}

		entity.ReceiveDamage(damage);
	}
	public override void ReceiveDamage(float damage)
	{
		health -= damage;
		print(transform.name + " has " + health + " hp");

		if (IsDead() && !isDead)
		{
			resourceGenerationScr.animals.Remove(this);
			DieAnimation();
		}
	}
	public override void Die()
	{
		print("Time: " + Time.time + "   Die");
		Instantiate(meatPrefab, transform.position, Quaternion.identity);
		//Instantiate(dieParticlesPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}


}
