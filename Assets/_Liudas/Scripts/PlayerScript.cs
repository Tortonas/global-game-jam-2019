using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerScript : EntityScript
{
	// Player along with all the animals must have "entity" layer

	public GameObject bloodParticlePrefab;
	public MainGroundScript mainGroundScript;
	public Transform playerModel;

	public Text hpText;

	int currentSpear = -1;
	public GameObject spearGO;
	public GameObject[] spears;

	public float healFrequency = 0.2f;
	public float healAmountEveryFrequency = 0.2f;

	public bool isFighting;
	public bool isGameActive;
	public bool isBuilding;
	public bool isNearCampFire;
    public bool isAlive = true;

	public LayerMask attackLayerMask;

	Weapon currentWeapon;
	NavMeshAgent playerAgent;

	private void Start()
	{
		if (playerModel == null)
			print("There is no player Modela atached");

		ShowHp();

		isGameActive = true;

		playerAgent = GetComponent<NavMeshAgent>();

		currentWeapon = Weapons.StoneSpear;

		// Do healing near campfire
		InvokeRepeating("CheckAndDoHealthRegeneration", healFrequency, healFrequency);

        canAttack = true;

    }
	private void Update()
	{
        if (isAlive)
        {
            // WASD input
            var inputX = Input.GetAxisRaw("Horizontal");
            var inputY = Input.GetAxisRaw("Vertical");
            var normalMovementVector = new Vector3(inputX, 0, inputY).normalized;

            // Rotation
            if (!isFighting)
            {
                if (normalMovementVector != Vector3.zero)
                    playerModel.rotation = Quaternion.Lerp(playerModel.rotation, Quaternion.LookRotation(normalMovementVector), Time.deltaTime * 10f);

            }
            else
            {
                /// TODO: look at hit rotation, play fight animation
            }

            // Movement
            playerAgent.Move(normalMovementVector * Time.deltaTime * movementSpeed);

            CheckAndDoAttacking();
        }
	}

	void CheckAndDoHealthRegeneration()
	{
		if (!isGameActive)
			return;

		if (isNearCampFire)
		{
			health += healAmountEveryFrequency;

			if (health > maxHealth)
				health = maxHealth;

			ShowHp();
		}
			

	}
	void CheckAndDoAttacking()
	{
		// Attacking
		if (Input.GetMouseButtonDown(0) && !isBuilding && canAttack)
		{
            canAttack = false;
            Invoke("ResetCanAttack", currentWeapon.FireRate);
            // Weapon settings
            var damage = currentWeapon.Damage;
			float distance = currentWeapon.Range;

			float offsetY = 0.5f;

			// Camera screen to world
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit))
			{
				// player pos
				var playerPositionForRaycasting = new Vector3(playerModel.position.x, offsetY, playerModel.position.z);

				// from camera hit pos
				var hitPos = hit.point;
				hitPos.y = offsetY;

				// the direction from hitpos to player
				var direction = hitPos - playerPositionForRaycasting;

				RaycastHit hit2;
				var ray2 = new Ray(playerPositionForRaycasting, direction.normalized);

				if (Physics.Raycast(ray2, out hit2, currentWeapon.Range, attackLayerMask))
				{
					print(hit2.transform.name);
					var entityComponent = hit2.transform.GetComponent<EntityScript>();
                    var particles = hit2.transform.GetComponent<Damageable>().ParticlePrefab;
                    var go = Instantiate(particles);
                    go.transform.position = hit2.point;

					// this error is harmless
					if (!entityComponent.isDead)
						entityComponent.ReceiveDamage(damage);
				}

				Debug.DrawRay(ray2.origin, ray2.direction.normalized * currentWeapon.Range, Color.blue, 5f);

			}
		}
	}

	void ShowHp()
	{
		hpText.text = string.Format("{0:0}", health);
	}

	public void SetWeapon(Weapon weapon)
	{
		if (currentSpear != -1)
		{
			SpawnSpearInHand();
			currentWeapon = weapon;
		} else
		{
			currentWeapon = Weapons.Fist;
		}

		currentSpear++;
	}
	void SpawnSpearInHand()
	{
		if (spearGO != null)
			DestroyImmediate(spearGO);

		spearGO = Instantiate(spears[currentSpear], transform.position, playerModel.rotation);
		spearGO.transform.SetParent(playerModel);
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
		ShowHp();
		Instantiate(bloodParticlePrefab, transform.position, transform.rotation);

		if (IsDead())
		{
			Die();
		}
	}

	public override void Die()
	{
		isGameActive = false;

		health = 0;
		ShowHp();

		GameObject.Find("Lose").GetComponent<LoseSystem>().PlayerDied();

        //gameObject.SetActive(false);
        isAlive = false;
        LoseSystem.instance.PlayerDied();
		Time.timeScale = 0;
		//Destroy(gameObject, 1);
	}

    void ResetCanAttack()
    {
        canAttack = true;
    }



}
