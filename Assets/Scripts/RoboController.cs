using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Player))]



public class RoboController : MonoBehaviour
{
    public Animator roboAnimator;

    float input_x = 0;

    float input_y = 0;

    public float speed = 5f;

    bool isWalking = false;

    Player player;

    Rigidbody2D rb2D;
    Vector2 movement = Vector2.zero;

    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        roboAnimator = GetComponent<Animator>();
        isWalking = true;
        rb2D = GetComponent<Rigidbody2D>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
         if (!canMove)
        {
        input_x = 0;
        input_y = 0;
        isWalking = false;
        movement = Vector2.zero;

        roboAnimator.SetBool("isWalking", false);
        return;
        }
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");
        isWalking = (input_x != 0 || input_y != 0);
        movement = new Vector2(input_x, input_y);

        if(isWalking){
           
            roboAnimator.SetFloat("input_x", input_x);
            roboAnimator.SetFloat("input_y", input_y);
        }

        roboAnimator.SetBool("isWalking", isWalking);

        
        if (player.entity.attackTimer < 0)
            player.entity.attackTimer = 0;
            else
                player.entity.attackTimer -= Time.deltaTime;

      
        
            if(Input.GetKeyDown(KeyCode.Z))
            {
            roboAnimator.SetTrigger("attack");
            

            Attack();
            }
        

    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * player.entity.speed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        if(player.entity.target == null)
        return;

        Guard guard = player.entity.target.GetComponent<Guard>();

        if(guard.entity.dead)
        {
            player.entity.target = null;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.entity.target.transform.position);

        if(distance <= player.entity.attackDistance)
        {
            int dmg = player.manager.CalculateDamage(player.entity, player.entity.damage);
            int enemyDef = player.manager.CalculateDefense(guard.entity, guard.entity.defense);
            int result = dmg - enemyDef;

            if (result < 0)
            result = 0;

            guard.entity.currentHealth -= result;

            guard.entity.target = this.gameObject;
        }
        


    }
}
