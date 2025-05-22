using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public Int32 CalculateHealth(Player player)
    {
        Int32 result = (player.entity.resistence * 10) + (player.entity.level * 4) + 10;
        return result;
    }

    public Int32 CalculateDamage(Player player, int damageSkill)
    {
        System.Random rnd = new System.Random();
        Int32 result = (player.entity.strength * 2) + (damageSkill* 2) + rnd.Next(1, 20);
        return result;
    }

}
