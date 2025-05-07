using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboController : MonoBehaviour
{
    public Animator roboAnimator;

    float input_x = 0;

    float input_y = 0;

    public float speed = 5f;

    bool isWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        roboAnimator = GetComponent<Animator>();
        isWalking = true;
    }

    // Update is called once per frame
    void Update()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");
        isWalking = (input_x != 0 || input_y != 0);

        if(isWalking){
            var move = new Vector3(input_x, input_y, 0).normalized;
            transform.position += move * speed * Time.deltaTime;
            roboAnimator.SetFloat("input_x", input_x);
            roboAnimator.SetFloat("input_y", input_y);
        }

        roboAnimator.SetBool("isWalking", isWalking);

        if(Input.GetButtonDown("Fire1")){
            roboAnimator.SetTrigger("attack");
        }

    }
}
