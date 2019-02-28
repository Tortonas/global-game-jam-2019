using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public string Name { get; private set; }
    public float Damage { get; private set; }
    public float Range { get; private set; }
    public float FireRate { get; private set; }
    public Weapon Next { get; private set; }

    public Weapon(float _damage, float _range, float _fireRate, string _name, Weapon _next)
    {
        Damage = _damage;
        Range = _range;
        FireRate = _fireRate;
        Name = _name;
        Next = _next;
    }

    static public bool operator ==(Weapon a, Weapon b)
    {
        return (a.Name == b.Name);
    }

    static public bool operator !=(Weapon a, Weapon b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        var weapon = obj as Weapon;
        return weapon != null &&
               Damage == weapon.Damage &&
               Range == weapon.Range &&
               FireRate == weapon.FireRate &&
               EqualityComparer<Weapon>.Default.Equals(Next, weapon.Next);
    }

    public override int GetHashCode()
    {
        var hashCode = -693049155;
        hashCode = hashCode * -1521134295 + Damage.GetHashCode();
        hashCode = hashCode * -1521134295 + Range.GetHashCode();
        hashCode = hashCode * -1521134295 + FireRate.GetHashCode();
        hashCode = hashCode * -1521134295 + EqualityComparer<Weapon>.Default.GetHashCode(Next);
        return hashCode;
    }
}
