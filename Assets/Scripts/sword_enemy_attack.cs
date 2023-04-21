using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword_enemy_attack : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy_sword;
    float cooldown = 0f;    //used in the reload cycle

    void attack(Vector3 direction){
        if (cooldown < 0){
            cooldown = 1f;
            GameObject sword_slash = Instantiate(enemy_sword, transform.position, transform.rotation);
            Destroy(sword_slash, 0.10f);
            sword_slash.GetComponent<Rigidbody2D>().velocity = -direction * 20;
        }
        cooldown -= Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        Vector3 distance_vector = transform.position - player.transform.position;
        Vector3 direction = distance_vector.normalized;
        float distance = distance_vector.magnitude;
        
        if (distance < 2.5) { attack(direction); }
    }
}
