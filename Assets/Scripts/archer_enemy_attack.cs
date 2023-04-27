using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class archer_enemy_attack : MonoBehaviour
{
    [SerializeField] int enemy_health = 1;
    [SerializeField] GameObject player;
    [SerializeField] GameObject arrow;
    [SerializeField] float arrow_speed = 15;
    [SerializeField] AudioSource arrow_audio;
    [SerializeField] AudioSource death_audio;
    float cooldown = 0f;    //used in the reload cycle


    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Projectile"){
            enemy_health--;
        }
    }

    void attack(Vector3 direction){
        if (cooldown < 0){
            cooldown = 1.5f;
            float theta = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Vector3 arrow_rotation = new Vector3(0f, 0f, theta);
            arrow_audio.Play();
            GameObject new_arrow = Instantiate(arrow, transform.position, Quaternion.Euler(arrow_rotation));
            Destroy(new_arrow, 3f);
            new_arrow.GetComponent<Rigidbody2D>().velocity = direction * arrow_speed;

        }
        cooldown -= Time.deltaTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (player == null && GameObject.FindGameObjectsWithTag("Player").Length != 0){
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        
    }

    // Update is called once per frame
    void Update(){

        if (player == null && GameObject.FindGameObjectsWithTag("Player").Length != 0){
            player = GameObject.FindGameObjectsWithTag("Player")[0];
        }
        

        if (enemy_health <= 0){
            death_audio.Play();
            Destroy(gameObject);
        }
    }

    void FixedUpdate() {

        if (player != null){
            Vector3 distance_vector = player.transform.position - transform.position;
            Vector3 direction = distance_vector.normalized;
            float distance = distance_vector.magnitude;
        
            if (distance < 10) {attack(direction);}
        }

        
    
    }
}
