using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    public AudioSource audioSource;

    public GameObject exitMap1;

    public GameObject exitMap2;

    public GameObject exitMap3;

    private bool canExit = false;
   

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

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            if (audioSource != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("AudioSource não atribuído ao Player!");
            }
            Destroy(collision.gameObject);
            Debug.Log("Item coletado!");

             if (exitMap1 != null) {
                exitMap1.SetActive(false); // Remove bloqueio

            canExit = true; // Libera troca de cena
             }
             if (exitMap2 != null)
                exitMap2.SetActive(false); // Remove bloqueio

             canExit = true; // Libera troca de cena

             if (exitMap3 != null)
                exitMap3.SetActive(false); // Remove bloqueio

             canExit = true; // Libera troca de cena
        }

        // Entra na área de saída
        if (collision.CompareTag("ExitMap1") && canExit)
        {
            SceneManager.LoadScene("SpaceLevel 2");
        }

        if (collision.CompareTag("ExitMap2") && canExit)
        {
            SceneManager.LoadScene("SpaceLevel 3");
        }

        if (collision.CompareTag("ExitMap3") && canExit)
        {
            SceneManager.LoadScene("SpaceLevel 4");
        }
    }
    

    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.transform.tag == "Guard")
        {
            player.entity.target = collider.transform.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.transform.tag == "Guard")
        {
            player.entity.target = null;
        }
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

            Debug.Log("ATAQUE");
            guard.entity.currentHealth -= result;

            guard.entity.target = this.gameObject;
        }
        


    }
}
