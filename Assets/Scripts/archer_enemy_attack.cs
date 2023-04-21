using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class archer_enemy_attack : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject arrow;
    [SerializeField] float arrow_speed = 15;
    float cooldown = 0f;    //used in the reload cycle

    void attack(Vector3 direction){
        if (cooldown < 0){
            cooldown = 1.5f;
            float theta = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Vector3 arrow_rotation = new Vector3(0f, 0f, theta);
            GameObject new_arrow = Instantiate(arrow, transform.position, Quaternion.Euler(arrow_rotation));
            Destroy(new_arrow, 3f);
            new_arrow.GetComponent<Rigidbody2D>().velocity = direction * arrow_speed;
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
        Vector3 distance_vector = player.transform.position - transform.position;
        Vector3 direction = distance_vector.normalized;
        float distance = distance_vector.magnitude;
        
        if (distance < 10) {attack(direction);}
    
    }
}
