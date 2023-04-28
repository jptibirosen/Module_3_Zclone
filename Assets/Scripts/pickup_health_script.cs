using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pickup_health_script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player" && GameManager.Instance.get_player_hp() < 3){
            GameManager.Instance.set_player_hp(3);
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start(){
        
        //makes sure it doesn't spawn when cleared rooms are reloaded
        string current_room_string = SceneManager.GetActiveScene().name;
        if (GameManager.Instance.room_status[current_room_string]){Destroy(gameObject);}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
