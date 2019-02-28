using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingHotkeys : MonoBehaviour
{
	public MainGroundScript mainGroundScript;

	private PlayerScript _player;
    private PlayerBuildingScript _building;
    private Weapon _currentWeapon;
    private bool _buildModeBarricade;
    private bool _buildModeTurret;
    private bool _buildModeTrap;

    private void Start()
    {
        var playerObject = GameObject.FindGameObjectWithTag("PlayerTag");
        _player = playerObject.GetComponent<PlayerScript>();
        _building = playerObject.GetComponent<PlayerBuildingScript>();
        _currentWeapon = Weapons.Fist;
		_player.SetWeapon(_currentWeapon);
        _buildModeBarricade = false;
        _buildModeTurret = false;
        _buildModeTrap = false;

    }

    private void Update()
    {
        BuildingHotkeys();
        SpearHotkey();
        BarricadeHotkey();
        TurretHotkey();
        TrapHotkey();
    }

    private void SpearHotkey()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && _currentWeapon != Weapons.StoneSpear)
        {
            if (CraftingSystem.CanCraftSpear())
            {
                CraftingSystem.CraftSpear();
                 _player.SetWeapon(_currentWeapon.Next);
                _currentWeapon = _currentWeapon.Next;
                Debug.Log(string.Format("CraftingSystem::Succesfully crafted a {0}", _currentWeapon.Name));
            }
        }
    }

    private void BarricadeHotkey()
    {
        if (_buildModeBarricade && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (CraftingSystem.CanCraftBarricade() && _building.BuildBarricade())
            {
                CraftingSystem.CraftBarricade();
				mainGroundScript.GenerateNavMeshes();

				//Debug.Log("CraftingSystem::Succesfully crafted a barricade");
            }
        }
    }

    private void TurretHotkey()
    {
        if (_buildModeTurret && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (CraftingSystem.CanCraftTurret() && _building.BuildTurret())
            {
                CraftingSystem.CraftTurret();
                Debug.Log("CraftingSystem::Succesfully crafted a turret");
            }
        }
    }

    private void TrapHotkey()
    {
        if (_buildModeTrap && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (CraftingSystem.CanCraftTrap() && _building.BuildTrap())
            {
                CraftingSystem.CraftTrap();
                Debug.Log("CraftingSystem::Succesfully crafted a trap");
            }
        }
    }

    private void BuildingHotkeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // double click - exit buildmode
            if (_buildModeBarricade)
            {
                CurrentState(false);
            }
            else // first click - get ready to build!
            {
                _player.isBuilding = true;
                _buildModeBarricade = true;
                _buildModeTurret = false;
                _buildModeTrap = false;
                _building.amIInBuildingMenu = true;
                _building.whatAmIBuilding = "Barricade";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // double click - exit buildmode
            if (_buildModeTurret)
            {
                CurrentState(false);
            }
            else // first click - get ready to build!
            {
                _player.isBuilding = true;
                _buildModeBarricade = false;
                _buildModeTurret = true;
                _buildModeTrap = false;
                _building.amIInBuildingMenu = true;
                _building.whatAmIBuilding = "Turret";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // double click - exit buildmode
            if (_buildModeTrap)
            {
                CurrentState(false);
            }
            else // first click - get ready to build!
            {
                _player.isBuilding = true;
                _buildModeTrap = true;
                _buildModeBarricade = false;
                _buildModeTurret = false;
                _building.amIInBuildingMenu = true;
                _building.whatAmIBuilding = "Trap";
            }
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.Mouse1)) || (Input.GetKeyDown(KeyCode.Alpha1)))
        {
            _player.isBuilding = false;
            _buildModeBarricade = false;
            _buildModeTurret = false;
            _buildModeTrap = false;
            _building.amIInBuildingMenu = false;
        }
    }

    void CurrentState(bool state)
    {
        _player.isBuilding = state;
        _buildModeBarricade = state;
        _buildModeTurret = state;
        _building.amIInBuildingMenu = state;
        _buildModeTrap = state;
    }
     


}
