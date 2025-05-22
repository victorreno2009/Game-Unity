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
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");
        isWalking = (input_x != 0 || input_y != 0);
        movement = new Vector2(input_x, input_y);

        if(isWalking){
           
            roboAnimator.SetFloat("input_x", input_x);
            roboAnimator.SetFloat("input_y", input_y);
        }

        roboAnimator.SetBool("isWalking", isWalking);

        if(Input.GetButtonDown("Fire1")){
            roboAnimator.SetTrigger("attack");
        }

    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * player.entity.speed * Time.fixedDeltaTime);
    }
}
