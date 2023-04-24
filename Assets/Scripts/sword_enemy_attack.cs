using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_enemy_attack : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy_sword;
    [SerializeField] AudioSource windup_audio;
    [SerializeField] AudioSource sword_attack_audio;
    float cooldown = 0f;    //used in the reload cycle
    

    
    //enum Enemy_state {Idle, Moving, Attacking};

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
        if (cooldown < 0){
            cooldown = 2.0f;
            //there is a 0.5 second wind-up with audio cue
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);    //the enemy stops
            windup_audio.Play();
            //play animation
            Invoke("attack", 0.5f);
            
        }
        cooldown -= Time.deltaTime;
        
        
    }

    /*void move_toward_player(Vector3 direction){
        Rigidbody2D rigid_2D = GetComponent<Rigidbody2D>();

        if (this_enemy == Enemy_state.Idle){
            rigid_2D.velocity = direction * 150 * Time.deltaTime;
        }
        
    }*/

    // Start is called before the first frame update
    void Start()
    {   
        //Enemy_state this_enemy;
        //this_enemy = Enemy_state.Idle;
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        Vector3 distance_vector = player.transform.position - transform.position;
        Vector3 direction = distance_vector.normalized;
        float distance = distance_vector.magnitude;
        
        //if (distance < 6) { move_toward_player(direction); }
        if (distance < 2.5) { full_attack(); }
    }
}
