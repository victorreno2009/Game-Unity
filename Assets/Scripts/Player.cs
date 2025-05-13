using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
   
    public Entity entity;

    [Header("Game Manager")]

    public GameManager manager;

    [Header("Player UI")]

    public Slider health;



    void Start()
    {

        if (manager == null)
        {
            Debug.LogError("Precisa anexar o game manager aqui no player");
            return;
        }

        entity.maxHealth = manager.CalculateHealth(this);
        
        entity.currentHealth = entity.maxHealth;

        health.maxValue = entity.maxValue;
        health.value = entity.maxHealth;
    }

    private void Update()
    {
        health.value = entity.currentHealth;

        if(Input.GetKeyDown(KeyCode.Space))
        entity.currentHealth -= 1;
    }

    
}
