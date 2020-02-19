using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public int Health;
    public int Armor;
    
    public Damage(int health, int armor)
    {
        Health = health;
        Armor = armor;
    }
}

public class DamageCalculator
{
    
    public static Damage DealDamage(int damage, IModifiersHolder damagerModifiers, IHealth target, IModifiersHolder targetModifiers)
    {
        int realDamage = damage;
        foreach (var modifier in targetModifiers.Modifiers)
        {
            realDamage = modifier.Value.ModifyTakeDamage(realDamage);
        }
        foreach (var modifier in damagerModifiers.Modifiers)
        {
            realDamage = modifier.Value.ModifyDealDamage(realDamage);
        }

        if (realDamage > target.Armor)
        {
            return new Damage(
                realDamage - target.Armor,
                target.Armor
            );
        }
        else
        {
            return new Damage(
                0,
                realDamage
            );
        }
    }

}
