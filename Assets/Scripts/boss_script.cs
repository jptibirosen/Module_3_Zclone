using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class boss_script : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject smoke;
    [SerializeField] Animator anim;
    [SerializeField] GameObject face;
    [SerializeField] AudioSource windup_audio;
    [SerializeField] AudioSource fireball_audio;
    [SerializeField] AudioSource hit_audio;



    float cooldown = 0f;
    float projectile_speed = 10;
    bool stage_2 = false;
    int attack_chain = 0;   //used in the stage_2 attack



    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Projectile"){
            hit_audio.Play();
            GameManager.Instance.set_boss_hp(-1);
            if (stage_2){teleport();}

            if (GameManager.Instance.get_boss_hp() <=0){
                if (!stage_2) {
                    stage_2 = true;
                    GameManager.Instance.set_boss_hp(5);
                    teleport();
                }

                else if (stage_2) {
                    //Debug.Log("you win");
                    GameManager.Instance.load_scene("room_win");
                }
            }
        }
    }


    Vector3 random_position(){
        /*returns a Vector3 with random coordinates in one of the three quadrants where the boss isn't
        used in the teleport ability*/

        //find out in which quadrant is the boss - origo is (2, -1.5)
        string our_quadrant = "q1";

        float boss_x = transform.position.x;
        float boss_y = transform.position.y;

        if (boss_x >= 2){
            if (boss_y >= -1.5){our_quadrant = "q1";}
            if (boss_y < -1.5){our_quadrant = "q2";}
        }
        if (boss_x < 2){
            if (boss_y < -1.5){our_quadrant = "q3";}
            if (boss_y >= -1.5){our_quadrant = "q4";}
            
        }

        //get a pair of random values for x and y within the boundaries of a quadrant
        System.Random rand = new System.Random();
        float rand_x = (float)(rand.NextDouble() * 8);
        float rand_y = (float)(rand.NextDouble() * 4.5);

        //apply the random coordinates in every quadrant except the current one and pick one randomly
        if (our_quadrant == "q1"){
            Vector3[] vectors_1 = {
                new Vector3(2 + rand_x, -1.5f - rand_y, 0f),
                new Vector3(2 - rand_x, -1.5f - rand_y, 0f),
                new Vector3(2 - rand_x, -1.5f + rand_y, 0f)
            };
            return vectors_1[rand.Next(0, vectors_1.Length)];
        }

        if (our_quadrant == "q2"){
            Vector3[] vectors_1 = {
                new Vector3(2 + rand_x, -1.5f + rand_y, 0f),
                new Vector3(2 - rand_x, -1.5f - rand_y, 0f),
                new Vector3(2 - rand_x, -1.5f + rand_y, 0f)
            };
            return vectors_1[rand.Next(0, vectors_1.Length)];
        }

        if (our_quadrant == "q3"){
            Vector3[] vectors_1 = {
                new Vector3(2 + rand_x, -1.5f + rand_y, 0f),
                new Vector3(2 + rand_x, -1.5f - rand_y, 0f),
                new Vector3(2 - rand_x, -1.5f + rand_y, 0f)
            };
            return vectors_1[rand.Next(0, vectors_1.Length)];
        }

        if (our_quadrant == "q4"){
            Vector3[] vectors_1 = {
                new Vector3(2 + rand_x, -1.5f + rand_y, 0f),
                new Vector3(2 + rand_x, -1.5f - rand_y, 0f),
                new Vector3(2 - rand_x, -1.5f - rand_y, 0f)
            };
            return vectors_1[rand.Next(0, vectors_1.Length)];
        }

        else return new Vector3(0f, 0f, 0f);
    }


    void teleport(){

        //play animation
        GameObject new_smoke = Instantiate<GameObject>(smoke, transform.position, transform.rotation);
        Destroy(new_smoke, 0.7f);

        //move to a random new position
        transform.position = random_position();
        cooldown = 2f;
    }

    void simple_attack(){
        //launches a fireball towards the player
        
        //get the direction of the player (normalized vector)
        Vector3 distance_vector = player.transform.position - transform.position;
        Vector3 direction = distance_vector.normalized;
        float distance = distance_vector.magnitude;

        //get the angle between this direction and the x-axis and rotate the projectile accordingly
        float theta = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Vector3 projectile_rotation = new Vector3(0f, 0f, theta + 90);

        fireball_audio.Play();
        GameObject new_fireball = Instantiate(fireball, transform.position, Quaternion.Euler(projectile_rotation));
        new_fireball.GetComponent<Rigidbody2D>().velocity = direction * projectile_speed;

        anim.SetTrigger("finish_attack");
    }

    void big_attack(){
        //launches 8 fireballs in the main and secondary cardinal directions 

        anim.SetTrigger("start_attack");

        Vector3[] directions = {
            new Vector3(0f, 1f, 0f).normalized,
            new Vector3(1f, 1f, 0f).normalized,
            new Vector3(1f, 0f, 0f).normalized,
            new Vector3(1f, -1f, 0f).normalized,
            new Vector3(0f, -1f, 0f).normalized,
            new Vector3(-1f, -1f, 0f).normalized,
            new Vector3(-1f, 0f, 0f).normalized,
            new Vector3(-1f, 1f, 0f).normalized
        };

        foreach (Vector3 direction in directions){

            //get the angle between this direction and the x-axis and rotate the projectile accordingly
            float theta = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Vector3 projectile_rotation = new Vector3(0f, 0f, theta + 90);

            fireball_audio.Play();
            GameObject new_fireball = Instantiate(fireball, transform.position, Quaternion.Euler(projectile_rotation));
            new_fireball.GetComponent<Rigidbody2D>().velocity = direction * projectile_speed;
        }

        anim.SetTrigger("finish_attack");
    }

    void attack(){

        if (cooldown < 0 && player != null){
            cooldown = 2f;

            if (!stage_2){
                anim.SetTrigger("start_attack");
                Invoke("simple_attack", 0.2f);
            }

            if(stage_2){
                if (attack_chain < 3){
                    anim.SetTrigger("start_attack");
                    Invoke("simple_attack", 0.2f);
                    attack_chain++;
                }
                else if (attack_chain >= 3){
                    attack_chain = 0;
                    windup_audio.Play();
                    anim.SetTrigger("start_windup");
                    Invoke("big_attack", 0.8f);
                }
            }

                
                

        }
        cooldown -= Time.deltaTime;
    }


    // Start is called before the first frame update
    void Start()
    {
        if (player == null && GameObject.FindGameObjectsWithTag("Player").Length != 0){
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }

        Instantiate<GameObject>(face);
        GameManager.Instance.set_boss_hp(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null && GameObject.FindGameObjectsWithTag("Player").Length != 0){
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }

        attack();
    }
}
