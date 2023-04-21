using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_attack : MonoBehaviour
{

    [SerializeField] GameObject sword;
    [SerializeField] Animator animator;
    [SerializeField] float weapon_delay = 0.15f;
    [SerializeField] float weapon_speed = 30f;

    bool weapon_ready = true;
    float weapon_busy = 0.6f; //this is the weapon cooldown time


    void set_weapon_ready(){
        weapon_ready = true;
    }

    void stop_attacking(){
        animator.SetBool("attacking_up", false);
        animator.SetBool("attacking_down", false);
        animator.SetBool("attacking_left", false);
        animator.SetBool("attacking_right", false);
    } 

    void attack_sword(string direction){
        Vector3 sword_velocity = new Vector3(0, 0, 0);

        if (direction == "left" && weapon_ready) {
            sword_velocity = new Vector3(-20, 0, 0);
            animator.SetBool("attacking_left", true);
            Invoke("stop_attacking", 0.06f);
        }
        if (direction == "right" && weapon_ready) {
            sword_velocity = new Vector3(20, 0, 0);
            animator.SetBool("attacking_right", true);
            Invoke("stop_attacking", 0.06f);
        }
        if (direction == "up" && weapon_ready) {
            sword_velocity = new Vector3(0, 20, 0);
            animator.SetBool("attacking_up", true);
            Invoke("stop_attacking", 0.06f);
        }
        if (direction == "down" && weapon_ready) {
            sword_velocity = new Vector3(0, -20, 0);
            animator.SetBool("attacking_down", true);
            Invoke("stop_attacking", 0.06f);
        }
            
        if (weapon_ready){
            GameObject new_sword = Instantiate<GameObject>(
                sword, transform.position, Quaternion.Euler(0f, 0f, 0f));
            new_sword.GetComponent<Rigidbody2D>().velocity = sword_velocity;
            Destroy(new_sword, weapon_delay);  
            weapon_ready = false;
            Invoke("set_weapon_ready", weapon_busy); 
            
        }
    }

    void sword_attack_input(){
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftArrow)){
            attack_sword("left");
        }
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.RightArrow)){
            attack_sword("right");
        }
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.UpArrow)){
            attack_sword("up");
        }
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.DownArrow)){
            attack_sword("down");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sword_attack_input();
    }
}
