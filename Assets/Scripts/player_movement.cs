using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{

    [SerializeField] float speed = 175;
    [SerializeField] Animator animator;

    void movement(){
        Rigidbody2D rigid_2D = GetComponent<Rigidbody2D>();

        if (Input.GetKey(KeyCode.RightArrow)){
            animator.SetBool("walking_right", true);
            rigid_2D.velocity = new Vector3(speed * Time.fixedDeltaTime, 0f, 0f);
        } else{
            animator.SetBool("walking_right", false);
        }
        if (Input.GetKey(KeyCode.LeftArrow)){
            animator.SetBool("walking_left", true);
            rigid_2D.velocity = new Vector3(-speed * Time.fixedDeltaTime, 0f, 0f);
        } else{
            animator.SetBool("walking_left", false);
        }
        if (Input.GetKey(KeyCode.UpArrow)){
            animator.SetBool("walking_up", true);
            rigid_2D.velocity = new Vector3(0f, speed * Time.fixedDeltaTime, 0f);
        } else{
            animator.SetBool("walking_up", false);
        }
        if (Input.GetKey(KeyCode.DownArrow)){
            animator.SetBool("walking_down", true);
            rigid_2D.velocity = new Vector3(0f, -speed * Time.fixedDeltaTime, 0f);
        }else{
            animator.SetBool("walking_down", false);
        }


        if (
            !(Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetKey(KeyCode.UpArrow)
            || Input.GetKey(KeyCode.DownArrow))){
                rigid_2D.velocity = new Vector3(0f, 0f, 0f);

            }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        
        movement();

    }
}
