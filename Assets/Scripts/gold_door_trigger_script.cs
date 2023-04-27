using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gold_door_trigger_script : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player"){
            if (GameManager.Instance.has_gold_key){
                GameManager.Instance.opened_gold_door = true;
                GameManager.Instance.open_door("Gold");
                //Destroy(gameObject);
            }
            else if (!GameManager.Instance.has_gold_key){
             Debug.Log("you need the gold key");
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
