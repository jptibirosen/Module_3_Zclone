using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_enemy_attack : MonoBehaviour
{
    [SerializeField] int enemy_health = 2;
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy_sword;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource windup_audio;
    [SerializeField] AudioSource sword_attack_audio;
    [SerializeField] AudioSource hit_audio;
    [SerializeField] AudioSource death_audio;
    float cooldown = 0f;    //used in the reload cycle
    

    
    enum Enemy_state {Passive, Active};
    Enemy_state this_enemy = Enemy_state.Active;

    void set_to_idle(){
        //this_enemy = Enemy_state.Passive;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        anim.SetBool("idle", true);
        anim.SetBool("attacking", false);
        anim.SetBool("moving_left", false);
        anim.SetBool("moving_right", false);
        anim.SetBool("moving_up", false);
        anim.SetBool("moving_down", false); 
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Projectile"){
            hit_audio.Play();
            enemy_health--;
        }
    }
    
    

    void attack(){  //spawns a projectile moving in the direction of the player
        Vector3 distance_vector = player.transform.position - transform.position;
        Vector3 direction = distance_vector.normalized;

        //proper rotation for the projectile
        float theta = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;   //sprite is oriented along y-axis
        Vector3 sword_rotation = new Vector3(0f, 0f, theta);

        sword_attack_audio.Play();
        GameObject sword_slash = Instantiate(enemy_sword, transform.position, Quaternion.Euler(sword_rotation));
        Destroy(sword_slash, 0.18f);
        sword_slash.GetComponent<Rigidbody2D>().velocity = direction * 15;    
    }

    void full_attack(){
        //the enemy stops
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0); 
        anim.SetBool("attacking", true);          
        //there is a 0.5 second wind-up with audio cue
        windup_audio.Play();
        Invoke("attack", 0.5f);
        Invoke("set_to_idle", 0.7f);    
    }

    void move_toward_player(Vector3 direction){
        Rigidbody2D rigid_2D = GetComponent<Rigidbody2D>();
        rigid_2D.velocity = direction * 150 * Time.deltaTime;        
    }

    // Start is called before the first frame update
    void Start()
    {   
        //Enemy_state this_enemy;
        //this_enemy = Enemy_state.Idle;
        anim = GetComponent<Animator>();

        if (player == null && GameObject.FindGameObjectsWithTag("Player").Length != 0){
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }
    }

    // Update is called once per frame
    void Update(){
        if (player == null && GameObject.FindGameObjectsWithTag("Player").Length != 0){
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }

        cooldown -= Time.deltaTime;
        if (cooldown < 0){
            this_enemy = Enemy_state.Active;
        }

        if (enemy_health <= 0) {
            death_audio.Play();
            Destroy(gameObject);
        }
    }

    void FixedUpdate() {
        if (player != null){
            Vector3 distance_vector = player.transform.position - transform.position;
            Vector3 direction = distance_vector.normalized;
            float distance = distance_vector.magnitude;

            if (this_enemy == Enemy_state.Active){
                if (5.5 < distance) { set_to_idle(); }
                if (distance <= 5.5) { move_toward_player(direction); }
                if (distance < 2) { 
                    full_attack();
                    cooldown = 2.0f; 
                    this_enemy = Enemy_state.Passive;
                } 
            }
        }

        
        
    }
}
