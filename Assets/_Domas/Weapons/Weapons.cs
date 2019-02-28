using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Weapons
{
    public static readonly Weapon StoneSpear = new Weapon(5f, 5f, 0.25f, "StoneSpear", null);
    public static readonly Weapon Spear = new Weapon(3f, 3f, 0.25f,"Spear", StoneSpear);
    public static readonly Weapon Fist = new Weapon(1f, 2f, 0.5f, "Fist", Spear);

}
