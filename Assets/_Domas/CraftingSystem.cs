using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CraftingSystem
{
    /// <summary>
    /// Takes the amount of resources based on item cost and crafts it.
    /// </summary>
    public static void CraftSpear()
    {
        // If you have enough resources, do 
        Resources.AddStone(-CraftingCosts.Spear_Stone);
        Resources.AddWood(-CraftingCosts.Spear_Wood);

        // Update spear costs
        CraftingCosts.Spear_Stone = 10;
    }

    /// <summary>
    /// Check if you have enough resources to craft a spear.
    /// </summary>
    /// <returns></returns>
    public static bool CanCraftSpear()
    {
        return (Resources.Stone >= CraftingCosts.Spear_Stone &&
                Resources.Wood >= CraftingCosts.Spear_Wood);
    }

    /// <summary>
    /// Takes the amount of resources based on item cost and crafts it.
    /// </summary>
    public static void CraftBarricade()
    {
        Resources.AddStone(-CraftingCosts.Barricade_Stone);
    }

    /// <summary>
    /// Check if you have enough resources to craft a barricade.
    /// </summary>
    /// <returns></returns>
    public static bool CanCraftBarricade()
    {
        return (Resources.Stone >= CraftingCosts.Barricade_Stone);
    }

    /// <summary>
    /// Takes the amount of resources based on item cost and crafts it.
    /// </summary>
    public static void CraftTurret()
    {
        Resources.AddFood(-CraftingCosts.Turret_Food);
        CraftingCosts.Turret_Food += 5;
    }

    /// <summary>
    /// Check if you have enough resources to craft a turret.
    /// </summary>
    /// <returns></returns>
    public static bool CanCraftTurret()
    {
        return (Resources.Food >= CraftingCosts.Turret_Food);
    }

    public static void CraftTrap()
    {
        Resources.AddWood(-CraftingCosts.Trap_Wood);
    }

    /// <summary>
    /// Check if you have enough resources to craft a trap.
    /// </summary>
    /// <returns></returns>
    public static bool CanCraftTrap()
    {
        return (Resources.Wood >= CraftingCosts.Trap_Wood);
    }
}
