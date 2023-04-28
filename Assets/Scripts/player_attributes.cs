using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_attributes : MonoBehaviour
{   
    [SerializeField] int player_health;
    [SerializeField] GameObject shield;

    bool is_blocking = false;
    bool shield_ready = true;

    bool god_mode = false;

    private void toggle_god_mode(){
        if (Input.GetKeyDown(KeyCode.I)){
            god_mode = !god_mode;
        }
    }


    private void stop_blocking(){
        is_blocking = false;
    }
    private void ready_shield(){
        shield_ready = true;
    }

    private void start_blocking(){
        if (Input.GetKeyDown(KeyCode.LeftAlt) && shield_ready){
            is_blocking = true;
            shield_ready = false;
            Invoke("stop_blocking", 0.5f);
            Invoke("ready_shield", 1.5f);

            GameObject new_shield = Instantiate<GameObject>(shield, transform.position, transform.rotation);
            Destroy(new_shield, 0.5f);
        }
    }


    void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log("player was hit");
        if (other.gameObject.tag == "Projectile" && !is_blocking && !god_mode){
            Destroy(other.gameObject);
            GameManager.Instance.set_player_hp(-1);
            //Debug.Log("player was hit by projectile");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        start_blocking();
        toggle_god_mode();
    }
}
