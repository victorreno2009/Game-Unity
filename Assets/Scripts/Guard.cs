using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class Guard : MonoBehaviour
{
    [Header("Controller")]
    public Entity entity = new Entity();
    public GameManager manager;

    [Header("Patrol")]
    public Transform[] wayPointList;
    public float arrivalDistance = 0.5f;
    public float waitTime = 5;

    //Privates
    Transform targetWayPoint;
    int currentWayPoint = 0;
    float lastDistanceToTarget = 0f;
    float currentWaitTime = 0f;

    [Header("Respawn")]
    public GameObject prefab;
    public bool respawn = true;
    public float respawnTime = 10f;

    [Header("UI")]
    public Slider healthSlider;

    public BoxCollider2D exitCollider;

    Rigidbody2D rb2D;
    Animator animator;

    private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            manager = GameObject.Find("GameManager").GetComponent<GameManager>();

            entity.maxHealth = manager.CalculateHealth(entity);
            entity.currentHealth = entity.maxHealth;

           // healthSlider.value = entity.currentHealth;

            currentWaitTime = waitTime;
            if (wayPointList.Length > 0)
            {
                targetWayPoint = wayPointList[currentWayPoint];
                lastDistanceToTarget = Vector2.Distance(transform.position, targetWayPoint.position);
            }
        }

    private void Update()
    {
        if (entity.dead)
        return;

        if (entity.currentHealth <= 0)
        {
            entity.currentHealth = 0;
            Die();
        }

        //healthSlider.value = entity.currentHealth;

        if (!entity.inCombat)
        {
            if (wayPointList.Length > 0)
            {
                Patrol();
            }
            else
            {
              //  animator.SetBool("isWalking", false);
            }
        }
        else
        {
            if (entity.attackTimer > 0)
                entity.attackTimer -= Time.deltaTime;

            if (entity.attackTimer < 0)
                entity.attackTimer = 0;

            if (entity.target != null && entity.inCombat)
            {
                // atacar
                if(!entity.combatCoroutine)
                StartCoroutine(Attack());
            }
            else
            {
                entity.combatCoroutine = false;
                StopCoroutine(Attack());
            }    
        }
    }

    

    private void OnTriggerStay2D(Collider2D collider) 
    {
        if(collider.tag == "Player" && !entity.dead)//!entity.inCombat)
        {
            // entity.attackTimer = entity.attackSpeed;
            entity.target = collider.gameObject;
            entity.inCombat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            entity.inCombat = false;
            entity.target = null;
        }
    }

    void Patrol()
    {
        if (entity.dead)
        return;

        //calcular a distância do wayPoint
        float distanceToTarget = Vector2.Distance(transform.position, targetWayPoint.position);

        if(distanceToTarget <= arrivalDistance || distanceToTarget >= lastDistanceToTarget)
        {
            

            if(currentWaitTime <= 0)
            {
                currentWayPoint++;

                if(currentWayPoint >= wayPointList.Length)
                currentWayPoint = 0;

                targetWayPoint = wayPointList[currentWayPoint];
                lastDistanceToTarget = Vector2.Distance(transform.position, targetWayPoint.position);

                currentWaitTime = waitTime;
            }
            else
            {
                currentWaitTime -= Time.deltaTime;
            }
        }
        else
        {
           // animator.SetBool("isWalking", true);
            lastDistanceToTarget = distanceToTarget;
        }

        Vector2 direction = (targetWayPoint.position - transform.position).normalized;
        
        rb2D.MovePosition(rb2D.position + direction * (entity.speed * Time.fixedDeltaTime));
        
    }

    IEnumerator Attack()
    {
        entity.combatCoroutine = true;

        while (true)
        {
            yield return new WaitForSeconds(entity.cooldown);

            if (entity.target != null && !entity.target.GetComponent<Player>().entity.dead)
            {
              //  animator.SetBool("attack", true);

                float distance = Vector2.Distance(entity.target.transform.position, transform.position);

                if (distance <= entity.attackDistance)
                {
                    int dmg = manager.CalculateDamage(entity, entity.damage);
                    int targetDef = manager.CalculateDefense(entity.target.GetComponent<Player>().entity, entity.target.GetComponent<Player>().entity.defense);
                    int dmgResult = dmg - targetDef;

                    if (dmgResult < 0)
                     dmgResult = 0;

                    entity.target.GetComponent<Player>().entity.currentHealth -= dmgResult;
                }
            }
        }
    }

 void Die()
{
    entity.dead = true;
    entity.inCombat = false;
    entity.target = null;

    animator.SetBool("isWalking", false);

    StopAllCoroutines();
    StartCoroutine(HandleGuardDeath());
}

IEnumerator HandleGuardDeath()
{
    Destroy(gameObject);
    yield return null; // espera 1 frame

    GameObject[] guards = GameObject.FindGameObjectsWithTag("Guard");
    Debug.Log("Restam " + guards.Length + " guards.");

    if (guards.Length == 0)
    {
        exitCollider.enabled = true;
        Debug.Log("Todos os guards morreram. Saída liberada!");
    }
}



    
}
