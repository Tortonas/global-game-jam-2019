using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampfireScript : MonoBehaviour
{

    /// <summary>
    /// Reik nepamirst isikelt Fire particle per unity editoriu
    /// </summary>

    public float howHardCampfireDecreseas; //koeficientas kuris bus dauginamas is deltaTime mazinant gyvybes lauzo
	public float healDistanceMultiplier = 2f;
	public float mainCampfireHealth; //lauzo gyvybes [0;100]
    public float experienceMultiplier; //patirties didinimo koeficientas bus dauginamas is lauzo gyvybiu

    public PlayerScript playerObject;
    [HideInInspector] public float maximumDistance;

    public ParticleSystem fireParticle;
    public Light pointLight;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //-- jeigu atsirastu bugas, kad hp virsys 100 arba bus maziau nei 0
        if (mainCampfireHealth < 0)
        {
            mainCampfireHealth = 0;
        }
        else if (mainCampfireHealth > 100)
        {
            mainCampfireHealth = 100;
        }
        //Debug.Log("Health: " + mainCampfireHealth + " Score: " + Resources.Score);
        //--
        fireParticle.emissionRate = mainCampfireHealth;

        //experience didinimas bei lauzo gyvybiu mazinimas
        if (mainCampfireHealth > 0f && mainCampfireHealth <= 100f)
        {
            //score reguliuojamas Domo scripte
            Resources.AddScore(Time.deltaTime*mainCampfireHealth*experienceMultiplier);
            mainCampfireHealth = mainCampfireHealth - howHardCampfireDecreseas * Time.deltaTime;
            //Debug.Log("Daeina");
        }

        pointLight.range = mainCampfireHealth / 4f;

        // -- sistema kuri tikrina ar zaidjeas yra netoli lauzo ir jeigu yra, siuncia i kita scripta duomenis

        Vector3 playerPosition = playerObject.transform.position;
        playerPosition.y = 0f;
        Vector3 campfirePosition = gameObject.transform.position;
        campfirePosition.y = 0f;

        Vector3 distanceVector = playerPosition - campfirePosition;

        //Debug.Log("Player: " + playerPosition.magnitude + " Campfire: " + campfirePosition.magnitude);

        // ---- nustatys maximum distance pagal pointLight.range
        maximumDistance = pointLight.range * 3.5f / 25f * healDistanceMultiplier;

        // ----

        if (distanceVector.magnitude < maximumDistance)
        {
            playerObject.isNearCampFire = true;

            //Debug.Log("Arti campfire " + distanceVector.magnitude);
        }
        else
        {
            playerObject.isNearCampFire = false;
            //Debug.Log("Toli nuo campfire " + distanceVector.magnitude);
        }

        // --

    }

    public void FeedFire()
    {
        mainCampfireHealth += 25f;
        Debug.Log("Increased fireplace life by 25");
    }
}
