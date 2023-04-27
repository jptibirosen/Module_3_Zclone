using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purple_door_trigger_script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            if (GameManager.Instance.has_purple_key){
                GameManager.Instance.opened_purple_door = true;
                GameManager.Instance.open_door("Purple");
                //Destroy(gameObject);
            }
            else if (!GameManager.Instance.has_purple_key){
             Debug.Log("you need the purple key");
            }
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
