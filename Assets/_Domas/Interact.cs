using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField]
    private float maximumRange = 3f;

    private CampfireScript _fire;
    public GameObject _genObject;
    private ResourceGeneration _gen;

    private Transform _playerModel;
    private PlayerScript _player;

    private void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("PlayerTag");
        _playerModel = temp.transform;
        _fire = GameObject.FindGameObjectWithTag("FireTag").GetComponent<CampfireScript>();
        _player = temp.GetComponent<PlayerScript>();
        _gen = _genObject.GetComponent<ResourceGeneration>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && _player.isBuilding == false)
        {
            var targetObject = CheckWhatIsHit();
            //Debug.Log(targetObject);
            if (targetObject == null)
                return;

            var targetName = targetObject.tag;
            switch (targetName)
            {
                case "WoodTag":
                    CollectWood(targetObject);
                    break;
                case "StoneTag":
                    CollectStone(targetObject);
                    break;
                case "TurretTag":
                    ReloadTurret(targetObject);
                    break;
                case "FireTag":
                    FeedFire(targetObject);
                    break;
                case "FoodTag":
                    CollectFood(targetObject);
                    break;
            }
        }
    }

    private void FeedFire(GameObject fire)
    {
        if (Resources.Wood <= 0)
            return;
        
        fire.GetComponent<CampfireScript>().FeedFire();
        //Maybe add some score, too?
        Resources.AddWood(-1);
    }

    private void CollectWood(GameObject twig)
    {
        Destroy(twig);
        // Maybe add some score, too?
        Resources.AddWood(1);
    }

    private void CollectStone(GameObject stone)
    {
        // Maybe add some score, too?
        Resources.AddStone(1);
        _gen.RemoveStone(stone);
        Destroy(stone);
    }

    private void CollectFood(GameObject food)
    {
        Destroy(food);
        // Maybe add some score, too?
        Resources.AddFood(1);
    }

    private void ReloadTurret(GameObject turret)
    {
        if (Resources.Stone <= 0)
            return;

        turret.GetComponent<TurretScript>().Reload();
        // Maybe add some score, too?
        Resources.AddStone(-1);
    }

    private GameObject CheckWhatIsHit()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            var rayStartPosition = hit.point;
            rayStartPosition.y = 20;

            Debug.DrawRay(rayStartPosition, Vector3.down * 50, Color.black, 5f);
            //Debug.Log(hit.transform.name);

            Vector3 playerPositionTemp = new Vector3(_playerModel.transform.position.x, 0f, _playerModel.transform.position.z);
            Vector3 clickPositionTemp = new Vector3(hit.point.x, 0f, hit.point.z);

            Vector3 vectorRange = playerPositionTemp - clickPositionTemp;

            //Debug.Log(vectorRange.magnitude);

            if (vectorRange.magnitude > maximumRange)
                return null;

            return hit.transform.gameObject;

        }
        return null;
    }
}

