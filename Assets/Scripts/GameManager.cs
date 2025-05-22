using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public Int32 CalculateHealth(Entity entity)
    {
        Int32 result = (entity.resistence * 10) + (entity.level * 4) + 10;
        return result;
    }

    public Int32 CalculateDamage(Entity entity, int damageSkill)
    {
        System.Random rnd = new System.Random();
        Int32 result = (entity.strength * 2) + (damageSkill* 2) + rnd.Next(1, 20);
        Debug.LogFormat("CalculateDamage: {0}", result);
        return result;
    }

    public Int32 CalculateDefense(Entity entity, int damageDefense)
    {
        System.Random rnd = new System.Random();
        Int32 result = (entity.strength * 2) + (damageDefense * 2) + rnd.Next(1, 20);
        Debug.LogFormat("CalculateDefense: {0}", result);
        return result;
    }

}
