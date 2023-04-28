using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_attack : MonoBehaviour
{
    //[SerializeField] int player_health = 3;
    [SerializeField] GameObject sword;
    [SerializeField] Animator animator;
    [SerializeField] float weapon_lifetime = 0.18f;
    [SerializeField] float weapon_speed = 15f;
    [SerializeField] AudioSource sword_attack_audio;
    [SerializeField] AudioSource hit_audio;

    bool weapon_ready = true;
    float weapon_busy = 0.6f; //this is the weapon cooldown time


    /*private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Projectile"){
            hit_audio.Play();
            player_health--;
        }
    }*/


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
        Vector3 sword_orientation = new Vector3(0, 0, 0);

        if (direction == "left" && weapon_ready) {
            sword_velocity = new Vector3(-weapon_speed, 0, 0);
            sword_orientation = new Vector3(0, 0, 90);
            animator.SetBool("attacking_left", true);
            Invoke("stop_attacking", 0.06f);
        }
        if (direction == "right" && weapon_ready) {
            sword_velocity = new Vector3(weapon_speed, 0, 0);
            sword_orientation = new Vector3(0, 0, 270);
            animator.SetBool("attacking_right", true);
            Invoke("stop_attacking", 0.06f);
        }
        if (direction == "up" && weapon_ready) {
            sword_velocity = new Vector3(0, weapon_speed, 0);
            animator.SetBool("attacking_up", true);
            Invoke("stop_attacking", 0.06f);
        }
        if (direction == "down" && weapon_ready) {
            sword_velocity = new Vector3(0, -weapon_speed, 0);
            sword_orientation = new Vector3(0, 0, 180);
            animator.SetBool("attacking_down", true);
            Invoke("stop_attacking", 0.06f);
        }
            
        if (weapon_ready){
            sword_attack_audio.Play();
            GameObject new_sword = Instantiate<GameObject>(
                sword, 
                transform.position, 
                Quaternion.Euler(sword_orientation));
            new_sword.GetComponent<Rigidbody2D>().velocity = sword_velocity;
            Destroy(new_sword, weapon_lifetime);  
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
