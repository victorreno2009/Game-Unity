using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Animator playerAnimator;

    float input_x = 0;

    float input_y = 0;

    public float speed = 5f;

    bool isWalking = false;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
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
            playerAnimator.SetFloat("input_x", input_x);
            playerAnimator.SetFloat("input_y", input_y);
        }

        playerAnimator.SetBool("isWalking", isWalking);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Planet1"))
        {
            SceneManager.LoadScene("Planet1Level");
        }
        if (other.CompareTag("Planet2"))
        {
            SceneManager.LoadScene("Planet2");
        }
        if (other.CompareTag("Planet3"))
        {
            SceneManager.LoadScene("Planet3");
        }

        if (other.CompareTag("Planet4"))
        {
            SceneManager.LoadScene("Planet4");
        }
        }
    }

