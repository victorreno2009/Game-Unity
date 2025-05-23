using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
   
    public Entity entity;

    [Header("Player Regen System")]

    public bool regenHPEnabled = true;

    public float regenHPTime = 15f;

    public int regenHPValue = 3;

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

        entity.maxHealth = manager.CalculateHealth(entity);

        Int32 dmg = manager.CalculateDamage(entity, 10); // Player
        Int32 def = manager.CalculateDamage(entity, 5); // Inimigo
        
        
        entity.currentHealth = entity.maxHealth;

        health.maxValue = entity.maxHealth;
        health.value = entity.maxHealth;

         // iniciar o regenHealth
        StartCoroutine(RegenHealth());

    }

   
    private void Update()
    {
        health.value = entity.currentHealth;

        if(Input.GetKeyDown(KeyCode.Space))
        entity.currentHealth -= 10;
    }

    IEnumerator RegenHealth()
    {
        while(true) // loop infinito
        {
            if(regenHPEnabled)
            {

            
            if(entity.currentHealth < entity.maxHealth)
            {
                entity.currentHealth += regenHPValue;
                yield return new WaitForSeconds(regenHPTime);
            }
            else
            {
                yield return null;
            }
            
        }
        else
            {
                yield return null;
            }
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Guard"))
    {
        TakeDamage(5); // Pode ajustar o valor conforme necessário
    }
}

  void TakeDamage(int amount)
   {
    entity.currentHealth -= amount;

    if (entity.currentHealth <= 0)
    {
        entity.currentHealth = 0;
        Die();
    }
    }

 void Die()
  {
    Debug.Log("O jogador morreu!");
    // Aqui você pode colocar lógica para morte, desativar jogador, mostrar UI de morte etc.
   }
}
